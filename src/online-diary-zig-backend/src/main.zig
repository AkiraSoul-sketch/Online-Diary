const std = @import("std");
const Init = std.process.Init;
const print = std.debug.print;
const Struct = std.builtin.Type.Struct;
const Declaration = std.builtin.Type.Declaration;

pub fn main(init: std.process.Init) void {                              
   var foo: Foo = Foo{};
   var deinitable: Deinitable(Foo) = Deinitable(Foo).wrap(&foo);
   deinitable.deinit();
   _ = init;
}

pub fn Deinitable(comptime T: type) type {         
   validateDeinitExistance(T);

   return struct {
      const Self: type = @This();
      instance: *T,

      pub fn wrap(reference: *T) Self {
         return Self{.instance = reference };
      }      

      pub fn deinit(self: Self) void {                           
         self.instance.deinit();         
      }      
   };   
}

//TODO: ensure that instance has decl AND decl with alloc parameter.
pub fn DeinitableWithAllocator(comptime T: type) type {
   validateDeinitExistance(T);

   return struct {
      const Self: type = @This();
      instance: *T,
      allocator: std.mem.Allocator,

      pub fn wrap(reference: *T, allocator: std.mem.Allocator) Self {
         return Self{ .allocator = allocator, .instance = reference };         
      }

      pub fn deinit(self: Self) void {         
         self.instance.deinit(self.allocator);                
      }
   };
}

pub fn DeinitableArrayList(comptime T: type) type {
   const List: type = std.array_list.Aligned(T, null);

   return struct {
      const Self: type = @This();
      instance: *List,
      allocator: std.mem.Allocator,

      pub fn wrap(reference: *List, allocator: std.mem.Allocator) Self {
         return Self{ .allocator = allocator, .instance = reference };
      }

      pub fn deinit(self: Self) void {
         self.instance.deinit(self.allocator);
      }
   };
}

pub fn DeinitableArrayListWithItems(comptime T: type) type {
   const List: type = std.array_list.Aligned([] const T, null);

   return struct {
      const Self: type = @This();
      instance: *List,
      allocator: std.mem.Allocator,

      pub fn wrap(reference: *List, allocator: std.mem.Allocator) Self {
         return Self{ .allocator = allocator, .instance = reference };
      }

      pub fn deinit(self: Self) void {
         for (self.instance.items) |item| {
            self.allocator.free(item);
         }

         self.instance.deinit(self.allocator);
      }
   };
}

fn validateDeinitExistance(comptime T: type) void {
   if (!@hasDecl(T, "deinit")) {   
      const error_message: []const u8 = @typeName(T) ++ " has no method: 'deinit() void'";
      @compileError(error_message);
   }
}

pub const Foo = struct {
   pub fn deinit(self: *Foo) void {
      _ = self;
   }
};