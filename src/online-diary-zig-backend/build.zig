const std = @import("std");
const httpz = @import("httpz");
const utils = @import("buildUtils.zig");


pub fn build(b: *std.Build) !void {
    try addChecksForAllFiles(b, "./libraries");
    const source_name: []const u8 = "main";
    const source_path: []const u8 = "src/main.zig";    
    const context = utils.createForExecutable(b, source_name, source_path, null);
    const run_check = b.step("check", "Check if compiles");

    context.addDependency("httpz", "httpz");

    const exe = context.createExecutable(source_name, true, true);                   
    b.installArtifact(exe);            

    // here put checks for libraries


    const exe_check = context.createExecutable(source_name, false, false);
    
    run_check.dependOn(&exe_check.step);
    
    const run_exe = b.addRunArtifact(exe);
    const run_step = b.step("run", "Run the program");
    run_step.dependOn(&run_exe.step);    
}

fn addChecksForAllFiles(b: *std.Build, sub_path: []const u8) !void {   
   const io = b.graph.io;
   const allocator = b.allocator;
   const cwd = std.Io.Dir.cwd();      
   const dir = try cwd.openDir(io, sub_path, .{.iterate = true});   
   var it = dir.iterate();   

   while (try it.next(io)) |entry| {    
     const name: [] const u8 = entry.name;         
     const full_path = std.fs.path.basename(name);
     std.debug.print("{s} - full path\n", .{full_path});       
     if (isZigFile(&entry)) std.debug.print("{s} - is zig file\n", .{name});       

     if (isDirectory(&entry))
     {        
        const next_path: []u8 = try std.fs.path.join(allocator, &.{sub_path, name});
        try addChecksForAllFiles(b, next_path);
     }     
   }   
}

fn isDirectory(entry: *const std.Io.Dir.Entry) bool {
    return entry.kind == .directory;
}

fn isZigFile(entry: *const std.Io.Dir.Entry) bool {
    if (entry.kind != .file) return false;    
    const extension: [] const u8 = std.fs.path.extension(entry.name);
    return std.mem.eql(u8, extension, ".zig");        
}