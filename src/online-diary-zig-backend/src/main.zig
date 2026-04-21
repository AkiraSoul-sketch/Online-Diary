const std = @import("std");
const Init = std.process.Init;
const print = std.debug.print;

pub fn main(init: std.process.Init) !void {            
   const io = init.io;
   const cwd = std.Io.Dir.cwd();      
   const dir = try cwd.openDir(io, "./", .{.iterate = true});
   var it = dir.iterate();
   while (try it.next(io)) |entry| {
      const name = entry.name;
      print("{s}\n", .{name});
   }   
}