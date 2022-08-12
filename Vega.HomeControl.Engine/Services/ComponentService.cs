using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Lifetime;
using Serilog;
using Vega.HomeControl.Api.Attributes.Components;
using Vega.HomeControl.Api.Data.Components;
using Vega.HomeControl.Api.Data.Config.Root;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Components;
using Vega.HomeControl.Api.Interfaces.Services;
using Vega.HomeControl.Components.Test.Entities;

namespace Vega.HomeControl.Engine.Services
{
    public class ComponentService : AbstractVegaEventService, IComponentService
    {
        private readonly IVegaConfig _config;
        private readonly List<AvailableComponent> _availableComponents;
        private readonly IDatabaseService _databaseService;
        private readonly ILifetimeScope _container;
        private readonly ISchedulerService _schedulerService;
        private List<IVegaComponent> _activeComponents = new();

        public ComponentService(ILogger logger, IEventBusService eventBusService, IVegaConfig vegaConfig,
            List<AvailableComponent> availableComponents,
            IDatabaseService databaseService,
            ISchedulerService schedulerService,
            ILifetimeScope container) : base(logger, eventBusService)
        {
            _config = vegaConfig;
            _schedulerService = schedulerService;
            _availableComponents = availableComponents;
            _databaseService = databaseService;
            _container = container;


            Init();

        }

        private async void Init()
        {
            StartComponents(_availableComponents);
        }

        private void StartComponents(List<AvailableComponent> availableComponents)
        {
            foreach (var availableComponent in availableComponents)
            {
                try
                {
                    StartComponent(availableComponent);
                }
                catch (Exception ex)
                {
                    Logger.Error("Error during initialize component {Component} : {Ex}", availableComponent.ComponentAttribute.Name, ex);
                }
            }
        }

        private void StartComponent(AvailableComponent component)
        {
            var resolvedComponent = _container.Resolve(component.ComponentType) as IVegaComponent;

            if (resolvedComponent is IVegaEmitterComponent emitter)
            {
                Logger.Information("Starting pooling for {Component}", component.ComponentAttribute.Name);
                var emitterDelay = GetEmitterDelay(resolvedComponent.GetType());
                _schedulerService.AddJob($"{component.ComponentAttribute.Name}_pool_job", async () => {
                    await emitter.Pool();
                }, (int)emitterDelay, false);
            }
        }

        private long GetEmitterDelay(Type component)
        {
            var attr = component.GetMethod(nameof(IVegaEmitterComponent.Pool)).GetCustomAttribute<EmitterDelayAttribute>();
            if (attr != null)
            {
                return attr.Interval;
            }

            return 60;
        }
    }
}
