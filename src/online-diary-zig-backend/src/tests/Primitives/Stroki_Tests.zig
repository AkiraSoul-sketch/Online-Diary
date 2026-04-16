const std = @import("std");
const expect = std.testing.expect;
const Stroki = @import("../../Common/Primitives/Stroki.zig");
const Stroka = Stroki.Stroka;

test "stroka_pustaya_tolko_probel_uspeh" {
    const pustaya_stroka: Stroka = Stroki.sozdat("");
    const pustaya: bool = pustaya_stroka.proverit_na_pustuyou_stroku();
    try expect(pustaya);
}

test "stroka_pustaya_tolko_probelami_uspeh" {
    const pustaya_stroka: Stroka = Stroki.sozdat("   ");
    const pustaya: bool = pustaya_stroka.proverit_na_pustuyou_stroku_ili_probeli();
    try expect(pustaya);
}

test "stroka_ne_pustaya_uspeh" {
    const ne_pustaya_stroka: Stroka = Stroki.sozdat("Привет");
    const pustaya: bool = ne_pustaya_stroka.proverit_na_pustuyou_stroku();
    try expect(!pustaya);
}

test "stroka_s_probelyami_i_ne_probelyami_uspeh" {
    const stroka: Stroka = Stroki.sozdat("  Привет  ");
    const pustaya: bool = stroka.proverit_na_pustuyou_stroku();
    try expect(!pustaya);
}

test "u8_pustoy_uspeh" {
    const stroka: []const u8 = "";
    const pustaya: bool = Stroki.proverit_na_pustuyou_stroku(stroka);
    try expect(pustaya);
}

test "u8_pustoy_probelami" {
    const stroka: []const u8 = "   ";
    const pustaya: bool = Stroki.proverit_na_pustuyou_stroku_ili_probeli(stroka);
    try expect(pustaya);
}
