//! Примитивы для работы с символами.
//!
//! Модуль содержит тип `Simvol` и функции для проверки,
//! является ли значение символом и является ли оно пробельным.

/// Объединение, которое содержит тип для представления символа.
// В данном случае `Simvol` — это объединение, которое может содержать либо Unicode символ, либо ASCII символ.
// Используется в целях читаемости и удобства, чтобы явно обозначить, что мы работаем с символами, а не просто с байтами.
// Объединение позволяет нам хранить разные типы данных в одном месте, что может быть полезно для оптимизации и упрощения кода,
// Особенно когда мы знаем, что символы могут быть представлены в разных форматах.
// enum означает, что мы можем использовать именованные варианты для доступа к данным внутри объединения,
// то есть мы можем обращаться к символу как к `Simvol.ascii_znachenie` или `Simvol.unicode_znachenie`.
pub const Simvol: type = union(enum) {
    unicode_znachenie: u21,
    ascii_znachenie: u8,
};

pub fn sozdat(comptime T: type, value: T) Simvol {
    return switch (T) {
        u8 => Simvol{ .ascii_znachenie = value },
        u21 => Simvol{ .unicode_znachenie = value },
        else => @compileError("Значение не является символом u8 или u21"),
    };
}

pub fn probel_ili_net(simvol: Simvol) bool {
    return switch (simvol) {
        // при помощи |byte| или |cp| происходит захват активного значения из объединения Simvol.
        // дальше взависимости от значения вызывается соответствующий метод.
        .ascii_znachenie => |byte| ascii_probel_ili_net(byte),
        .unicode_znachenie => |cp| unicode_probel_ili_net(cp),
    };

    // тоже самое что и
    // if (simvol == .ascii_znachenie) {
    //     return Probeli.ascii_probel_ili_net(simvol.ascii_znachenie);
    // }
    //     return Probeli.unicode_probel_ili_net(simvol.unicode_znachenie);
}

/// Проверяет, является ли тип символа ASCII или Unicode.
pub fn simvol_ili_net(comptime T: type) bool {
    return switch (T) {
        u8, u21 => true,
        else => false,
    };
}

/// Проверяет, является ли тип символа пробельным символом.
/// Проверяет на уровне ASCII.
fn ascii_probel_ili_net(byte: u8) bool {
    return switch (byte) {
        0x09...0x0D,
        0x20,
        => true,
        else => false,
    };
}

/// Проверяет, является ли Unicode code point пробельным символом.
/// Проверяет на уровне Unicode.
fn unicode_probel_ili_net(cp: u21) bool {
    return switch (cp) {
        0x0009...0x000D,
        0x0020,
        0x0085,
        0x00A0,
        0x1680,
        0x2000...0x200A,
        0x2028,
        0x2029,
        0x202F,
        0x205F,
        0x3000,
        => true,
        else => false,
    };
}
