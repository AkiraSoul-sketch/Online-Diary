const std = @import("std");

pub fn invoke() void {
    std.debug.print("hello world", .{});
}