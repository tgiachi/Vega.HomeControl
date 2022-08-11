using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class VaultService : AbstractVegaEventService, IVaultService
    {
        public VaultService(ILogger logger, IEventBusService eventBusService) : base(logger, eventBusService)
        {

        }
    }
}
