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

    const directories_mod = b.addModule("Directory", .{
        .root_source_file = b.path("src/Directory.zig"),
        .target = target,
        .optimize = optimize,
    });    

    const exe_mod = b.addModule("main", .{
        .root_source_file = b.path("src/main.zig"),
        .target = target,
        .optimize = optimize,
    });    

    exe_mod.addImport("Deinitables", deinitable_mod);
    exe_mod.addImport("Directory", directories_mod);

    const path_module = b.addTest(.{
        .name = "Path",
        .root_module = b.createModule(.{
            .root_source_file = b.path("src/Path.zig"),
            .target = target,
            .optimize = optimize
        })
    });

    const file_module = b.addTest(.{ 
        .name = "File",
        .root_module = b.createModule(.{
            .root_source_file = b.path("src/Files.zig"),
            .target = target,
            .optimize = optimize,
        })
    });    

    const directory_module = b.addTest(.{ 
        .name = "Directory", 
        .root_module = b.createModule(.{ 
            .root_source_file = b.path("src/Directory.zig"), 
            .target = target, 
            .optimize = optimize,            
        }) 
    });            

    file_module.root_module.addImport("Directory", directory_module.root_module);
    file_module.root_module.addImport("Path", path_module.root_module);

    directory_module.root_module.addImport("File", file_module.root_module);
    directory_module.root_module.addImport("Path", path_module.root_module);

    check.dependOn(&path_module.step);
    check.dependOn(&file_module.step);
    check.dependOn(&directory_module.step);

    const exe = b.addExecutable(.{
        .name = "main",
        .root_module = exe_mod,
        .use_lld = true,
        .use_llvm = true,
    });
    const exe_check = b.addExecutable(.{
        .name = "main",
        .root_module = exe_mod,
    });
    b.installArtifact(exe);    
    
    check.dependOn(&exe_check.step);
    const run_exe = b.addRunArtifact(exe);
    run_step.dependOn(&run_exe.step);
}
