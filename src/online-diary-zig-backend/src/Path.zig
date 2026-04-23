//! Высокоуровневая абстракция для работы с путями.
//! - Поддерживает пути к папкам и памяти.
//! - Поддерживает относительные пути и абсолютные.

const std = @import("std");

const Io = std.Io;
const Allocator = std.mem.Allocator;
const StdDir = Io.Dir;
const StdFile = Io.File;
const StdDirError = StdDir.OpenError;
const StdFileError = StdFile.OpenError;

const Self: type = @This();
owned: ?[:0]u8,
value: [] const u8,
allocator: Allocator,

pub const Error: type = error {
    InvalidPath,
    Unexpected
};

pub const Kind: type = enum {
    directory,
    file,

    pub fn init(path: *const Self, io: Io) Error!Kind {
        const dir_dummy: StdDir = StdDir{.handle = 0 };
        const stat = std.Io.Dir.statFile(dir_dummy, io, path.value, .{}) 
            catch return Error.Unexpected;
        return switch (stat.kind) {
            .directory => .directory,
            .file => .file,            
            else => Error.Unexpected,
        };   
    }

    pub fn isDirectory(self: *const Kind) bool {
        return self.* == .directory;
    }

    pub fn isFile(self: *const Kind) bool {
        return self.* == .file;
    }    
};

/// Инициализирует путь. Путь - это путь либо к папке либо к файлу.
/// Параметр path может быть как абсолютным путем, так и относительным (относительно текущего cwd).
/// Итоговый объект Path будет содержать owned память .owned и его представление value.
/// Запрещено работать с .owned напрямую.
pub fn init(path: []const u8, io: Io, allocator: Allocator) Error!Self {
    if (path.len == 0) return Error.InvalidPath;

    var cwd_path_absolute: Self = initCurrentAbsolute(io, allocator) 
        catch return Error.InvalidPath;
    defer cwd_path_absolute.deinit();

    const normalized: []const u8 = std.fs.path.resolve(allocator, &.{ cwd_path_absolute.value, path })
        catch return Error.InvalidPath;
    return Self{ .allocator = allocator, .value = normalized, .owned = null };
}

/// Инициализирует то же самое, что и `init()` но результатом будет абсолютный путь текущего процесса.
pub fn initCurrentAbsolute(io: Io, allocator: Allocator) Error!Self {
    const path = StdDir.cwd().realPathFileAlloc(io, ".", allocator)
        catch return Error.InvalidPath;     
    return Self { .allocator = allocator, .value = path, .owned = path };
}

/// Высвобождает память, занятую объектом. После вызова этого метода объект не пригоден к использованию.
pub fn deinit(self: * Self) void {
    if (self.owned != null) self.allocator.free(self.owned.?);  
    if (self.owned == null) self.allocator.free(self.value);      
    self.* = undefined;
}

/// Проверка на существование пути.
pub fn exists(self: *Self, io: Io) bool {                
    _ = Kind.init(self, io) catch return false;
    return true;    
}   

pub fn kind(self: * const Self, io: Io) Error!Kind {
    return Kind.init(self, io);
}

/// Вывод в дебаг консоль для отладки.
pub fn debugPrint(self: Self) void {
    std.debug.print("path: {s}\n", .{self.value});
}

test "init current path success" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    const current_cwd = try StdDir.cwd().realPathFileAlloc(io, ".", allocator);    
    std.debug.print("current cwd: {s}\n", .{current_cwd});
    defer allocator.free(current_cwd);

    var current: Self = try initCurrentAbsolute(io, allocator);
    current.debugPrint();
    defer current.deinit();

    try std.testing.expect(std.mem.findDiff(u8, current_cwd, current.value) == null);
}

test "inited path exists" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    var current: Self = try init("./src", io, allocator);    
    defer current.deinit();

    try std.testing.expect(current.exists(io));
}

test "inited directory path exists" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    var current: Self = try init("./src", io, allocator);    
    defer current.deinit();
    
    try std.testing.expect(current.exists(io));
}

test "inited file path exists" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    var current: Self = try init("./src/main.zig", io, allocator);
    defer current.deinit();

    try std.testing.expect(current.exists(io));
}

test "inited path does not exists" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    var current: Self = try init("./src/not_existed", io, allocator);    
    defer current.deinit();

    try std.testing.expect(!current.exists(io));
}

test "inited path is absolute success" {
    const io: Io = std.testing.io;
    const allocator: Allocator = std.testing.allocator;

    var current: Self = try initCurrentAbsolute(io, allocator);    
    defer current.deinit();

    try std.testing.expect(std.fs.path.isAbsolute(current.value));
}