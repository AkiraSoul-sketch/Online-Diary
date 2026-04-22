const std = @import("std");
const httpz = @import("httpz");
const utils = @import("buildUtils.zig");
const uuid = @import("./libraries/uuid7/uuid7.zig");

// TODO: добавить анализ для всех файлов:
// 1. добавляем анализируемые файлы как тест (b.addTest)
// 2. используем step check на этих файлах.
// для этого нужно получить пути к этим файлам.

pub fn build(b: *std.Build) !void {
    const optimize = b.standardOptimizeOption(.{});
    const target = b.standardTargetOptions(.{});

    const check = b.step("check", "check if compiles");
    const run_step = b.step("run", "Run program");

    const deinitable_mod = b.addModule("Deinitables", .{
        .root_source_file = b.path("src/Deinitables.zig"),
        .target = target,
        .optimize = optimize,
    });

    const directories_mod = b.addModule("Directories", .{
        .root_source_file = b.path("src/Directories.zig"),
        .target = target,
        .optimize = optimize,
    });                

    const exe_mod = b.addModule("main", .{
        .root_source_file = b.path("src/main.zig"),
        .target = target,
        .optimize = optimize,        
    });
    exe_mod.addImport("Deinitables", deinitable_mod);
    exe_mod.addImport("Directories", directories_mod);    

    const modules_check = b.addTest(.{
        .name = "check",
        .root_module = b.createModule(.{
            .root_source_file = b.path("src/Directories.zig"),
            .target = target,
            .optimize = optimize
        })
    });

    const exe = b.addExecutable(.{
        .name = "main",
        .root_module = exe_mod,
        .use_lld = true,
        .use_llvm = true,
    });
    b.installArtifact(exe);

    const exe_check = b.addExecutable(.{
        .name = "main",
        .root_module = exe_mod,        
    });    
    
    check.dependOn(&modules_check.step);
    check.dependOn(&exe_check.step);        
    const run_exe = b.addRunArtifact(exe);
    run_step.dependOn(&run_exe.step);             
}