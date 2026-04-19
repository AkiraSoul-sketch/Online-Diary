const std = @import("std");
const expect = std.testing.expect;
const print = std.debug.print;

const Uuid7 = struct {    
    raw_value: u128,    

    pub fn generate(io: std.Io) Uuid7 {

        const combined = generateBits(io);        
        return Uuid7{ .raw_value = combined, };   

    }

    fn generateBits(io: std.Io) u128 {

        const clock = std.Io.Clock.real;
        const random = (std.Random.IoSource{.io = io}).interface();        

        const current_time_milliseconds: u48 = @intCast(clock.now(io).toMilliseconds());
        const variant: u2 = 0b10; // вариант - 2 бита.        
        const version: u4 = 0b0111; // версия - 7 бит.                
        const random_a: u12 = random.int(u12);
        const random_b: u62 = random.int(u62);
                
        return 
            (@as(u128, current_time_milliseconds) << 80) |
            (@as(u128, version) << 76) |
            (@as(u128, random_a) << 64) |
            (@as(u128, variant) << 62) |
            (@as(u128, random_b));  

    }

    fn toHexString(self: Uuid7, buffer: *[36]u8) []const u8 {

        var bytes: [16]u8 = undefined;
        std.mem.writeInt(u128, &bytes, self.raw_value, .big);
        const hex = std.fmt.bytesToHex(bytes, .upper);        

        return std.fmt.bufPrint(
            buffer,
            "{s}-{s}-{s}-{s}-{s}",
            .{
                hex[0..8],
                hex[8..12],
                hex[12..16],
                hex[16..20],
                hex[20..32],
            }
        ) catch unreachable;             

    }
};

test "generate_uuid7" {        
    const io = std.testing.io;
    const uuid = Uuid7.generate(io);            
    var buffer: [36]u8 = undefined;
    printLn("uuid v7: {s}", .{uuid.toHexString(&buffer)});    
    try expect(true); // placeholder.
}

fn printLn(comptime text: []const u8, args: anytype) void {
    std.debug.print(text ++ "\n", args);
}

fn printDigit(value: anytype) void {
    std.debug.print("{d}\n", .{value});
}