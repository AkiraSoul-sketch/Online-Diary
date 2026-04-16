const Simvoli = @import("Simvoli.zig");

// Импортируем, чтобы можно было использовать в Stroka метод proverit_na_pustuyou_stroku_ili_probeli
// В чем проблема:
// В Zig структура определяет свой namespace.
// Функции, которые есть в файле .zig но не внутри структуры - они находятся на верхнем уровне.
// Когда мы внутри структуры, пытаемся вызвать верхнеуровневую функцию, которая находится в том же файле И ИМЕЕТ ТАКОЕ ЖЕ НАЗВАНИЕ
// то Zig не понимает, какую конкретно функцию мы хотим вызвать.
// Поэтому мы используем @This() для получения доступа к текущему файлу и его функциям и структурам
// Чтобы контролировать, к какой структуре или функции мы хотим обратиттся
// по сути можно и писать @This().Функция(), а можно импортировать в константу.
const EtotFail = @This();

/// Строка. Обертка над массивом символов.
pub const Stroka: type = struct {
    /// Здесь мы храним строку (массив символов), которая может быть пустой или состоять только из пробелов.
    znachenie: []const u8,

    /// Метод, который возвращает длину строки.
    pub fn poluchit_dlinu(stroka: Stroka) usize {
        return stroka.znachenie.len;
    }

    /// Метод, который проверяет, является ли строка пустой или нет.
    pub fn proverit_na_pustuyou_stroku(stroka: Stroka) bool {
        return EtotFail.proverit_na_pustuyou_stroku(stroka.znachenie);
    }

    /// Метод, который проверяет, состоит ли строка только из пробелов или нет.
    pub fn proverit_na_pustuyou_stroku_ili_probeli(stroka: Stroka) bool {
        return EtotFail.proverit_na_pustuyou_stroku_ili_probeli(stroka.znachenie);
    }
};

pub fn sozdat(value: []const u8) Stroka {
    return Stroka{
        .znachenie = value,
    };
}

pub fn proverit_na_pustuyou_stroku(value: []const u8) bool {
    return value.len == 0;
}

/// Метод, который проверяет, состоит ли строка только из пробелов или нет.
pub fn proverit_na_pustuyou_stroku_ili_probeli(value: []const u8) bool {
    if (proverit_na_pustuyou_stroku(value)) {
        return true;
    }

    // Если строка состоит из 3 и более символов, то лучше применить цикл для проверок.
    var ne_probeli: i32 = 0;
    for (value, 0..) |_, index| {
        const simvol = Simvoli.sozdat(u8, value[index]);
        if (!Simvoli.probel_ili_net(simvol)) {
            return false;
        }
        ne_probeli += 1;
    }

    return ne_probeli != 0;
}
