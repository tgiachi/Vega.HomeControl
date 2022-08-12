using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Vega.HomeControl.Api.Data.Components;
using Vega.HomeControl.Api.Data.Config.Root;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class ComponentService : AbstractVegaEventService, IComponentService
    {
        private readonly IVegaConfig _config;
        private readonly List<AvailableComponent> _availableComponents;
        public ComponentService(ILogger logger, IEventBusService eventBusService, IVegaConfig vegaConfig, List<AvailableComponent> availableComponents) : base(logger, eventBusService)
        {
            _config = vegaConfig;
            _availableComponents = availableComponents;
        }
    }
}
