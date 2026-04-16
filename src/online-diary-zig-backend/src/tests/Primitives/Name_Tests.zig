const std = @import("std");
const testing = std.testing;
const Stroki = @import("../../Common/Primitives/Stroki.zig");
const Stroka = Stroki.Stroka;
const Name = @import("../../Common/ValueObjects/Name.zig");

test "sozdat_imya_uspeh" {
    const znachenie: []const u8 = "Test name";
    const stroka = Stroki.sozdat(znachenie);
    const name = try Name.sozdat_iz_stroka(stroka);
    try testing.expect(name.ravno_u8_const_array(znachenie));
}

test "sozdat_imya_s_pustoy_strokoy_fail" {
    const znachenie: []const u8 = "  ";
    try testing.expectError(Name.Name_Oshibki.Pustoe_Imya, Name.sozdat_iz_u8_const_array(znachenie));
}
