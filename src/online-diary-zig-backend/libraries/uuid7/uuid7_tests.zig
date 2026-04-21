const std = @import("std");
const Uuid7 = @import("uuid7.zig");
const testing = std.testing;

test "generate_uuid7" {                    
    const io = std.testing.io;
    const uuid: Uuid7 = Uuid7.generate(io);                        
    var buffer: [36]u8 = undefined;    
    const hex_string = try Uuid7.toHexString(&uuid, buffer[0..]);        
    try std.testing.expect(hex_string.len == 36);    
}

test "generate_hex_string_invalid_string_buffer" {
    const io = std.testing.io;
    const uuid: Uuid7 = Uuid7.generate(io);
    var buffer: [37]u8 = undefined;
    const result = uuid.toHexString(buffer[0..]);    
    try testing.expectError(Uuid7.Errors.InvalidStringBuffer, result);
}