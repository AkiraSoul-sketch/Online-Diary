const std = @import("std");
const Allocator = std.mem.Allocator;
const Type = std.builtin.Type;
const Declaration = Type.Declaration;
const Parameters = Type.Fn.Param;

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

pub fn DeinitableWithAllocator(comptime T: type) type {
   validateDeinitExistance(T);
   validateDeinitContainsAllocatorParameter(T);

   return struct {
      const Self: type = @This();
      instance: *T,
      allocator: Allocator,

      pub fn wrap(reference: *T, allocator: Allocator) Self {
         return Self{ .allocator = allocator, .instance = reference };         
      }

      pub fn deinit(self: Self) void {         
         self.allocator.free(self.instance);                   
      }
   };
}

pub fn DeinitableArrayList(comptime T: type) type {
   const List: type = std.array_list.Aligned(T, null);

   return struct {
      const Self: type = @This();
      instance: *List,
      allocator: Allocator,

      pub fn wrap(reference: *List, allocator: Allocator) Self {
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
      allocator: Allocator,

      pub fn wrap(reference: *List, allocator: Allocator) Self {
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

fn validateDeinitContainsAllocatorParameter(comptime T: type) void {
   const deinit_fn = @field(T, "deinit");
   const info: std.builtin.Type = @typeInfo(@TypeOf(deinit_fn));
   if (info != .@"fn") @compileError(@typeName(T) ++ ".deinit is not a function.");
   const parameters: [] const std.builtin.Type.Fn.Param = info.@"fn".params;
   if (parameters.len == 0) @compileError(@typeName(T) ++ "Deinit must contain alloc first parameter.");
   if (parameters[0] == null) @compileError(@typeName(T) ++ "Deinit must contain alloc first parameter.");
   if (parameters[0].? != std.mem.Allocator) @compileError(@typeName(T) ++ "Deinit must contain alloc first parameter.");   
}

fn validateDeinitExistance(comptime T: type) void {
   
   if (!@hasDecl(T, "deinit")) {   
      const error_message: []const u8 = @typeName(T) ++ " has no method: 'deinit() void'";
      @compileError(error_message);
   }
}