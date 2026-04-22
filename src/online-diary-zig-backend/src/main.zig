const std = @import("std");

pub fn main(init: std.process.Init) void {     
   _ = init;         
}

pub const Foo = struct {
   pub fn deinit(self: *Foo) void {
      _ = self;
   }   
};