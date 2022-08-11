using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Impl.Services
{
    public class AbstractBaseVegaService : IVegaService
    {
        protected ILogger Logger { get; }

        public AbstractBaseVegaService(ILogger logger)
        {
            Logger = logger;

        }

        public virtual async void Dispose()
        {
            Logger.Debug("Disposing service {Service}", GetType().Name);
            await Shutdown();
        }

        public virtual async ValueTask DisposeAsync()
        {
            Logger.Debug("Disposing service async {Service}", GetType().Name);
            await Shutdown();
        }



        public virtual Task Shutdown()
        {
            Logger.Debug("Shutdown service {Service}", GetType().Name);
            return Task.CompletedTask;

        }
    }
}
