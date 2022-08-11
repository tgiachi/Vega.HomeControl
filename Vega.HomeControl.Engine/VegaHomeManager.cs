using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using AutofacSerilogIntegration;
using Config.Net;
using Config.Net.Stores;
using MediatR.Extensions.Autofac.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;
using Vega.HomeControl.Api.Attributes.Core;
using Vega.HomeControl.Api.Data.Config.Root;
using Vega.HomeControl.Api.Data.Directories;
using Vega.HomeControl.Api.Data.Events.Application;
using Vega.HomeControl.Api.Data.Events.Impl;
using Vega.HomeControl.Api.Interfaces.Services;
using Vega.HomeControl.Api.Utils;
using Vega.HomeControl.Engine.Modules;
using Vega.HomeControl.Engine.Services;
using Module = Autofac.Module;

namespace Vega.HomeControl.Engine
{
    public class VegaHomeManager
    {
        private readonly ContainerBuilder _containerBuilder;
       
        private readonly IFileSystemService _fileSystemService;
        private IContainer _container;
        private SystemDirectories _systemDirectories;
        private ILogger _logger;
        private IVegaConfig _vegaConfig;

        private readonly string _rootDirectory;
        private readonly bool _enableDebug;

        public VegaHomeManager()
        {
            AssemblyUtils.AddAssembly(typeof(DefaultServiceModuleLoader).Assembly);

            _rootDirectory = Environment.GetEnvironmentVariable("VEGA_HOME_DIRECTORY") ??
                             Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VegaHome");

            // _enableDebug = Environment.GetEnvironmentVariable("VEGA_LOG_DEBUG") != null;
            _enableDebug = true;
            _fileSystemService = new FileSystemService(_rootDirectory);
            _containerBuilder = new ContainerBuilder();

            InitializeSystemDirectories();
            InitLogger();
            InitConfig();
            InitAssemblies();
            InitContainer();
        }

        public Task Start()
        {
            Log.Logger.Information("Root directory: {RootDirectory}", _rootDirectory);
            var eventBus = _container.Resolve<IEventBusService>();

            eventBus.PublishNotification(new VegaNotificationEvent<ApplicationReadyEvent>(GetType().Name, new ApplicationReadyEvent()));

            return Task.CompletedTask;
        }

        private void InitConfig()
        {
            _logger.Information("Loading config vega.json");

            var configStore = new JsonConfigStore(Path.Join(_rootDirectory, _systemDirectories[SystemDirectoryType.Configs], "vega.json"), true);
            _vegaConfig = new ConfigurationBuilder<IVegaConfig>()
                .UseEnvironmentVariables()
                .UseConfigStore(configStore)
                .Build();

            if (!_vegaConfig.Common.IsConfigured)
            {
                _logger.Information("Application not configured saving default config");
                _vegaConfig.Common.IsConfigured = true;
                //_vegaConfig.Assemblies.AssembliesToLoad = new List<string>();

            }
        }

        private void InitializeSystemDirectories()
        {
            _systemDirectories = new SystemDirectories
            {
                [SystemDirectoryType.Root] = _rootDirectory,
            };

            foreach (var dirType in (SystemDirectoryType[])Enum.GetValues(typeof(SystemDirectoryType)))
            {
                if (dirType != SystemDirectoryType.Root)
                {
                    _systemDirectories[dirType] =
                        Environment.GetEnvironmentVariable($"VEGA_{dirType.ToString().ToUpper()}_DIRECTORY") ??
                        dirType.ToString().ToLower();
                }
            }

            _fileSystemService.InitializeSystemDirectories(_systemDirectories);
        }

        private void InitLogger()
        {
            var loggerConfiguration = new LoggerConfiguration();
            loggerConfiguration = _enableDebug ? loggerConfiguration.MinimumLevel.Debug() : loggerConfiguration.MinimumLevel.Information();
            loggerConfiguration = loggerConfiguration.Enrich.FromLogContext();

            loggerConfiguration = loggerConfiguration.WriteTo.Console();
            loggerConfiguration = loggerConfiguration.WriteTo.File(
                Path.Join(_rootDirectory, _systemDirectories[SystemDirectoryType.Logs], "vega_home.log"),
                rollingInterval: RollingInterval.Day);

            Log.Logger = loggerConfiguration.CreateLogger();

            _logger = Log.ForContext<VegaHomeManager>();

            _containerBuilder.RegisterLogger();
        }

        private void InitAssemblies()
        {

            _logger.Information("Loading additional {Count} assemblies", _vegaConfig.Assemblies.AssembliesToLoad.ToList().Count);
            foreach (var assembly in _vegaConfig.Assemblies.AssembliesToLoad)
            {
                _logger.Information("Loading additional assembly: {Assembly}", assembly);

                try
                {
                    AssemblyUtils.AddAssembly(Assembly.Load(assembly));
                }
                catch (Exception ex)
                {
                    _logger.Error("Error during loading request assembly: {Assembly} => {Ex}", assembly, ex);
                }
            }
        }

        private void InitContainer()
        {
            _containerBuilder.RegisterInstance(_fileSystemService).As<IFileSystemService>();
            _containerBuilder.RegisterInstance(_vegaConfig);
            _containerBuilder.RegisterInstance(_systemDirectories);

            _containerBuilder.RegisterType<EventBusService>().As<IEventBusService>().SingleInstance();

            _logger.Debug("Loading service modules");

            foreach (var module in AssemblyUtils.GetAttribute<VegaModuleLoaderAttribute>())
            {
                try
                {
                    _logger.Information("Loading module: {Module}", module.Name);
                    _containerBuilder.RegisterModule((Activator.CreateInstance(module) as Module)!);
                }
                catch (Exception ex)
                {
                    _logger.Error("Error during module: {Module}, are you sure have implemented Module class from Autofac?: {Err}", module.FullName, ex);
                }
            }

            _logger.Information("Registering Event bus");

            _containerBuilder.RegisterMediatR(AssemblyUtils.GetAppAssemblies());

            _container = _containerBuilder.Build();
        }
    }
}
