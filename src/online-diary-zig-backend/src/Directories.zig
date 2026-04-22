const std = @import("std");
const Dir = std.Io.Dir;
const Io = std.Io;
const Iterator = std.Io.Dir.Iterator;
const Allocator = std.mem.Allocator;
const testing = std.testing;

pub fn Directory() type {    

    return struct {
        const Self = @This();  
        path: [:0]const u8,
        name: []const u8,
        children_paths: [][]const u8,
        parent_path: ?[]const u8,
        allocator: std.mem.Allocator,

        const current = Self{
            
        };

        pub fn deinit(self: *Self) void {
            for (self.children_paths) |child| {
                self.allocator.free(child);
            }

            if (self.parent_path != null) {
                self.allocator.free(self.parent_path.?);
            }            

            self.allocator.free(self.children_paths);
            self.allocator.free(self.path);                                    
            self.* = undefined;
        }

        pub fn init(io: Io, allocator: Allocator, path: []const u8) !Self {
            const cwd: Dir = std.Io.Dir.cwd();        
            const dir: Dir = try cwd.openDir(io, path, .{ .iterate = true });
            defer dir.close(io);

            const directory_path: [:0]const u8 = try dir.realPathFileAlloc(io, path, allocator);
            std.debug.print("init directory path: {s}\n", .{ directory_path });
            errdefer allocator.free(directory_path);

            const directory_name: []const u8 = std.fs.path.basename(directory_path);
            std.debug.print("init directory name: {s}\n", .{directory_name});

            const parent_path: ?[]const u8 =
                if (std.mem.eql(u8, path, "."))
                    null
                else
                    std.fs.path.dirname(directory_path);

            var children_list = std.ArrayList([]const u8).empty;
            errdefer {
                for (children_list.items) |child| {
                    allocator.free(child);
                }
                children_list.deinit(allocator);
            }

            var iterator: Iterator = dir.iterate();                        
            while (try iterator.next(io)) |entry| {
                if (entry.kind != .directory) continue;                
                const children_path: []const u8 = try std.fs.path.join(allocator, &.{ directory_path, entry.name });
                children_list.append(allocator, children_path) catch |err| {
                    allocator.free(children_path);
                    return err;
                };
                std.debug.print("children path: {s}\n", .{children_path});
            }       

            const children: [][]const u8 = try children_list.toOwnedSlice(allocator);
            defer children_list.deinit(allocator);

            return Self{
                .name = directory_name,
                .parent_path = parent_path,
                .children_paths = children,
                .path = directory_path,
                .allocator = allocator,
            };
        }        
    };    
}

test "compile api" {    
    const allocator = testing.allocator;
    const io = testing.io;

    var dir: Directory() = try Directory().init(io, allocator,".");
    dir.deinit();
}