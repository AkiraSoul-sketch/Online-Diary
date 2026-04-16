const Stroki = @import("../Primitives/Stroki.zig");
const Oshibka_Vvoda = @import("../Errors/Vvod_Oshibka.zig");
const std = @import("std");
const Stroka = Stroki.Stroka;

/// Структура для представления имени. Содержит одно поле - строку, которая хранит значение имени.
pub const Name: type = struct {
    znachenie: Stroka,

    /// Переименовать строку.
    /// Ошибка `Pustoe_Imya`, если строка пустая или состоит только из пробелов.
    pub fn pereimenovat(_: Name, novoe: Stroka) Name_Oshibki.Pustoe_Imya!Name {
        return switch (novoe.proverit_na_pustuyou_stroku_ili_probeli()) {
            true => Name_Oshibki.Pustoe_Imya,
            false => Name{ .znachenie = novoe },
        };
    }

    pub fn ravno(self: Name, drugoe: Name) bool {
        return ravno_stroke(self, drugoe.znachenie);
    }

    pub fn ravno_stroke(self: Name, stroka: Stroka) bool {
        return ravno_u8_const_array(self, stroka.znachenie);
    }

    pub fn ravno_u8_const_array(self: Name, array: []const u8) bool {
        return std.mem.eql(u8, self.znachenie.znachenie, array);
    }
};

/// Создать имя из `Stroka`.
/// Ошибка `Pustoe_Imya`, если строка пустая или состоит только из пробелов.
pub fn sozdat_iz_stroka(value: Stroka) Name_Oshibki.Pustoe_Imya!Name {
    return switch (value.proverit_na_pustuyou_stroku_ili_probeli()) {
        true => Name_Oshibki.Pustoe_Imya,
        false => Name{ .znachenie = value },
    };
}

/// Создать имя из массива байтов.
/// Ошибка `Pustoe_Imya`, если массив байтов пустой или состоит из пробелов.
pub fn sozdat_iz_u8_const_array(value: []const u8) Name_Oshibki.Pustoe_Imya!Name {
    const stroka: Stroka = Stroki.sozdat(value);
    return sozdat_iz_stroka(stroka);
}

pub const Name_Oshibki = error{
    Pustoe_Imya,
};
