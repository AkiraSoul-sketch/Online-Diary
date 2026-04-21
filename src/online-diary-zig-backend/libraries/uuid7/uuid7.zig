//! Глобальный уникальный идентификатор v7
//! Хранит 128-битовое UUID значение и предоставляет функционал генерации uuid.

const std = @import("std");
const Uuid7 = @This();

raw_value: u128,

/// Создать строку из глобального уникального идентификатора.
/// Если строка не 36 символов - ошибка.
pub fn toHexString(self: *const Uuid7, buffer: []u8) Errors![]const u8 {
    if (buffer.len != 36) return Errors.InvalidStringBuffer;

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

/// Сгенерировать глобальный уникальный идентификатор.
pub fn generate(io: std.Io) Uuid7 {
    const combined = generateBits(io);        
    return Uuid7{ .raw_value = combined, };   
}

/// Создать глобальный идентификатор из строки (конвертировать строку в UUID7)
pub fn fromString(comptime input: [] const u8) Uuid7 {
    _ = input;
    @compileError("implemenet method");
}

/// Сгенерировать биты для генерации глобального уникального идентификатора.
/// Не является публичным API.
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

pub const Errors: type = error {
    InvalidStringBuffer
};