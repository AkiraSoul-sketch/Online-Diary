const std = @import("std");
const Dir = std.Io.Dir;
const Io = std.Io;
const Iterator = std.Io.Dir.Iterator;
const Allocator = std.mem.Allocator;
const testing = std.testing;
const OpenError = Dir.OpenError;
const Path = @import("Path.zig");

const Self: type = @This();
path: Path,

pub const Dependencies: type = struct {    
    io: Io,
    allocator: Allocator,
};

pub const Error: type = error{    
    NotDirectory,
} || Path.Error;

pub fn init(path: *const Path, io: Io) Error!Self {  
    const kind: Path.Kind = try path.kind(io);
    return switch (kind.isDirectory()) {
        true => Self{.path = try Path.init(path.value, io, path.allocator) },
        false => Error.NotDirectory
    };    
}

pub fn initFromString(input: []const u8, io: Io, allocator: Allocator) Error!Self {
    const path: Path = try Path.init(input, io, allocator);
    defer path.deinit();
    return init(&path, io);
}

pub fn deinit(self: *Self) void {    
    self.path.deinit();
    self.* = undefined;
}

test "create current directory success" {
    const io = std.testing.io;
    const allocator = std.testing.allocator;

    var path: Path = try Path.initCurrentAbsolute(io, allocator);
    defer path.deinit();

    var directory: Self = try init(&path, io);
    defer directory.deinit();
}
