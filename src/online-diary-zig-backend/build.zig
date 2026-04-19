const std = @import("std");

pub fn build(b: *std.Build) !void {
    const optimize = b.standardOptimizeOption(.{ .preferred_optimize_mode = .Debug });
    const target = b.standardTargetOptions(.{ });          
    const main_path = b.path("src/main.zig");

    const exe = b.addExecutable(.{
        .name = "main",
        .root_module = b.createModule(.{
            .root_source_file = main_path,
            .target = target,
            .optimize = optimize,
        })        
    });   

    b.installArtifact(exe);

    const run_exe = b.addRunArtifact(exe);
    const run_step = b.step("run", "Run the program");
    run_step.dependOn(&run_exe.step);
}