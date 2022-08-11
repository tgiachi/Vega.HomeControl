using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using NLua.Event;
using NLua.Exceptions;
using Serilog;
using Vega.HomeControl.Api.Data.Directories;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class ScriptEngineService : AbstractVegaEventService, IScriptEngineService
    {

        private readonly IFileSystemService _fileSystemService;
        private readonly SystemDirectories _systemDirectories;
        private Lua _scriptEngine;

        public ScriptEngineService(ILogger logger, IEventBusService eventBusService, IFileSystemService fileSystemService, SystemDirectories systemDirectories) : base(logger, eventBusService)
        {
            _systemDirectories = systemDirectories;
            _fileSystemService = fileSystemService;

            Init();
        }

        public void Init()
        {
            Logger.Information("Initializing LUA script engine");
            _scriptEngine = new Lua();
            _scriptEngine.State.Encoding = Encoding.UTF8;
            _scriptEngine.HookException += ScriptEngineOnHookException;
            AddPackages();

            ScanForScripts();


        }

        private void AddPackages()
        {
            _scriptEngine["packages"] = ";" +_systemDirectories.GetFullPath(SystemDirectoryType.Packages) + $"{Path.DirectorySeparatorChar}?.lua;";
            _scriptEngine.DoString($"package.path = package.path .. packages");
        }

        private async void ScanForScripts()
        {
            Logger.Information("Scanning for LUA scripts");
            var files = _fileSystemService.ScanDirectory(_systemDirectories.GetFullPath(SystemDirectoryType.Scripts), "*.lua");
            Logger.Information("Found {Count} LUA scripts!", files.Count);
            foreach (var file in files)
            {
                Logger.Information("Loading LUA script {File}", file.FileName);
                await LoadFile(file.FullFileName);
            }
        }

        public Task LoadString(string value)
        {
            try
            {
                _scriptEngine.DoString(value);
            }
            catch (LuaException exception)
            {
                Logger.Error("Error during execute script: {Ex}", exception);
            }

            return Task.CompletedTask;
        }

        public Task LoadFile(string fileName)
        {
            return LoadString(File.ReadAllText(fileName));
        }

        private void ScriptEngineOnHookException(object? sender, HookExceptionEventArgs e)
        {
            Logger.Error("Error during execute LUA script: {Error}", e.Exception);
        }
    }
}
