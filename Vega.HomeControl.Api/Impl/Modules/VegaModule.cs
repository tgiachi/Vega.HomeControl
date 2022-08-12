using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Vega.HomeControl.Api.Interfaces.Modules;

namespace Vega.HomeControl.Api.Impl.Modules
{
    public class VegaModule : Module, IVegaModule
    {
        protected ILogger Logger { get; }

        public VegaModule(ILogger logger)
        {
            Logger = logger;
        }

    }
}
