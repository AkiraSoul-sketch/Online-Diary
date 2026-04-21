const std = @import("std");
const httpz = @import("httpz");
const utils = @import("buildUtils.zig");


pub fn build(b: *std.Build) !void {
    const optimize = b.standardOptimizeOption(.{});
    const target = b.standardTargetOptions(.{});        

    const exe_mod = b.addModule("main", .{        
        .root_source_file = b.path("src/main.zig"),
        .target = target,
        .optimize = optimize,                
    });        

    const exe = b.addExecutable(.{
        .use_lld = true,
        .use_llvm = true,
        .name = "main",
        .root_module = exe_mod,        
    });        
    
    const check = b.step("check", "check if compiles");    
    check.dependOn(&exe.step);    

    b.installArtifact(exe);    
    const run_exe = b.addRunArtifact(exe);
    const run_step = b.step("run", "Run program");
    run_step.dependOn(check);
    run_step.dependOn(&run_exe.step);    

    try addChecksForAllFiles(b);


    // const source_name: []const u8 = "main";
    // const source_path: []const u8 = "src/main.zig";    
    // const context = utils.createForExecutable(b, source_name, source_path, null);
    // const run_check = b.step("check", "Check if compiles");

    // context.addDependency("httpz", "httpz");

    // const exe = context.createExecutable(source_name, true, true);                   
    // b.installArtifact(exe);            

    // here put checks for libraries

    // const exe_check = context.createExecutable(source_name, false, false);
    // run_check.dependOn(&exe_check.step);
    
    // const run_exe = b.addRunArtifact(exe);
    // const run_step = b.step("run", "Run the program");
    // run_step.dependOn(&run_exe.step);           
}

fn addChecksForAllFiles(b: *std.Build) !void {   
   const io = b.graph.io;
   const allocator = b.allocator;      
   var directory: Directory = try Directory.current_directory_with_allocator(allocator, io);
   const deinitable_directory: Deinitable = .{ .directory = &directory };   
   for (deinitable_directory.directory.children) |child| {
    std.debug.print("child directory: {s}\n", .{child});
   }      
}

fn isZigFile(entry: *const std.Io.Dir.Entry) bool {
    if (entry.kind != .file) return false;    
    const extension: [] const u8 = std.fs.path.extension(entry.name);
    return std.mem.eql(u8, extension, ".zig");        
}

pub const DirectoryFile = struct {    
    files: [][]const std.Io.File,
    allocator: std.mem.Allocator,

    pub fn deinit(self: *DirectoryFile) void {
        for (self.files) |file| {
            self.allocator.free(file);
        }

        self.allocator.free(self.files);
    }
};

const Directory: type = struct {
    parent: ?[]const u8,
    name: []const u8,
    path: []const u8,
    children: [][]const u8,
    allocator: std.mem.Allocator,    

    pub fn get_files(self: *Directory, allocator: std.mem.Allocator, io: std.Io) !void {        
        const dir: std.Io.Dir = std.Io.Dir.openDirAbsolute(io, self.path, .{ .iterate = true }) catch |err| {
            return handleDirectoryErrors(err);
        };        
        defer dir.close(io);        

        var iterator: std.Io.Dir.Iterator = dir.iterate();
        var paths_list = std.ArrayList([]const u8).empty;
        errdefer {
            for (paths_list.items) |path| allocator.free(path);
            paths_list.deinit(allocator);
        }

        while (iterator.next(io) catch |err| {
            return handleDirectoryErrors(err);
        }) |entry| {            
            if (entry.kind == .file) {                
                const path = self.path ++ entry.name;                
                try paths_list.append(allocator, path);                
            }
        }


    }

    pub fn current_directory_with_allocator(allocator: std.mem.Allocator, io: std.Io) DirectoryErrors!Directory {        
        const currentPath: *const[2:0]u8 = "./";
        const cwd: std.Io.Dir = std.Io.Dir.cwd();
        const directory_path: [:0]u8 = cwd.realPathFileAlloc(io, currentPath, allocator) catch |err| {            
            return handleDirectoryErrors(err);
        };               
        errdefer allocator.free(directory_path);
        
        const directory_name: []const u8 = getLastPathNode(directory_path);
        const working_dir: std.Io.Dir = cwd.openDir(io, currentPath, .{ .iterate = true }) catch |err| {
            return handleDirectoryErrors(err);
        };
        defer working_dir.close(io);        
       
        var iterator: std.Io.Dir.Iterator = working_dir.iterate();
        var children_list = std.ArrayList([]const u8).empty;
        errdefer {            
            for (children_list.items) |child| allocator.free(child);
            children_list.deinit(allocator);
        }

        while (iterator.next(io) catch |err| { 
            return handleDirectoryErrors(err);
        }) |entry| {                                                
            if (entry.kind == .directory) {
                const children_name: []const u8 = entry.name;
                const children_path: []u8 = try std.fs.path.join(allocator, &.{ directory_name, children_name });
                try children_list.append(allocator, children_path);
            }
        }        
        
        const children: [][]const u8 = try children_list.toOwnedSlice(allocator);
        return Directory { 
            .name = directory_name,
            .children = children,
            .path = directory_path,
            .parent = null,            
            .allocator = allocator
        };
    }

    pub fn deinit(self: *Directory) void {
        self.allocator.free(self.path);                
        if (self.parent != null) {
            self.allocator.free(self.parent);
        }

        for (self.children) |child| {
            self.allocator.free(child);
        }

        self.allocator.free(self.children);
    }

    fn getLastPathNode(path: []const u8) []const u8 {
        return std.fs.path.basename(path);        
    }    

    fn handleDirectoryErrors(err: std.Io.Dir.RealPathFileAllocError) DirectoryErrors {
        return switch (err) {
            error.FileNotFound => return DirectoryErrors.NotExists,
            error.NotDir => return DirectoryErrors.NotDirectory,
            error.BadPathName, 
            error.NameTooLong => return DirectoryErrors.InvalidPath,            
            else => err,
        };
    }    
};

pub fn freeArrayList(comptime T: type, allocator: std.mem.Allocator, list: *std.array_list.Aligned(T, null)) void {
   list.deinit(allocator);
}

pub fn freeArrayListWithItems(comptime T: type, allocator: std.mem.Allocator, list: *std.array_list.Aligned([]T, null)) void {
   for (list.items) |item| {
      allocator.free(item);
   }
   freeArrayList(T, allocator, list);
}

const DeinitableErrors: type = error{
    NoDeinitMethodFound
};

const Deinitable: type = union(enum) {
    directory: *Directory,    

    pub fn deinit(self: *Deinitable) void {
        switch (self) {            
            inline else => |case| return case.deinit(),
        }        
    }    
};

const DirectoryErrors = error{
    InvalidPath,
    NotDirectory,
    NotExists
} || std.Io.Dir.Iterator.Error || std.Io.Dir.OpenError || std.Io.Dir.RealPathFileAllocError;