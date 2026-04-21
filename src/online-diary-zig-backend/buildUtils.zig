const std = @import("std");
const Build = std.Build;
const Module = Build.Module;
const CreateOptions = Module.CreateOptions;
const OptimizeMode = std.builtin.OptimizeMode;


pub const BuildContext: type = struct {
    optimize: std.builtin.OptimizeMode,
    target: ?std.Build.ResolvedTarget,
    main_exe: *std.Build.Module,
    build: *std.Build,

    pub fn addDependency(self: *const BuildContext, name: []const u8, import_name: []const u8) void {
        const dependency = self.build.dependency(name, .{
            .target = self.target,
            .optimize = self.optimize
        });
        self.main_exe.addImport(name, dependency.module(import_name));
    }

    pub fn createExecutable(
        self: *const BuildContext, 
        name: []const u8,
        use_llvm: bool,
        use_lld: bool) *Build.Step.Compile {
        const options = createExeOptions(name, self, use_llvm, use_lld);
        const executable = self.build.addExecutable(options);
        return executable;
    }
};

pub fn createForExecutable(b: *Build,source_name: []const u8,source_file: []const u8, mode: ?OptimizeMode) BuildContext {        
    const optimize = createOptimizeMode(b, mode);        
    const create_options = createExeCreateOptions(b, source_file, optimize);
    const main_exe = b.addModule(source_name, create_options);    
    return BuildContext{
        .main_exe = main_exe,
        .optimize = optimize,
        .target = create_options.target,
        .build = b,        
    };
}

fn createOptimizeMode(b: *Build, mode: ?OptimizeMode) OptimizeMode {
    const resolved_mode = if (mode == null) .Debug else mode.?;        
    const optimize_options = Build.StandardOptimizeOptionOptions{
        .preferred_optimize_mode = resolved_mode 
    };                 

    return b.standardOptimizeOption(optimize_options);
}

fn createExeCreateOptions(
    b: *Build,
    source_file: []const u8,        
    optimize: OptimizeMode) CreateOptions {
    const target = b.standardTargetOptions(.{}); 
    const source_file_path = b.path(source_file);
    return CreateOptions{
        .root_source_file = source_file_path,
        .target = target,
        .optimize = optimize
    };
}

fn createExeOptions(
    name: []const u8, 
    context: *const BuildContext, 
    use_llvm: bool, 
    use_lld: bool) Build.ExecutableOptions {
    return Build.ExecutableOptions{
        .use_llvm = use_llvm,
        .use_lld = use_lld,
        .name = name,
        .root_module = context.main_exe
    };
}