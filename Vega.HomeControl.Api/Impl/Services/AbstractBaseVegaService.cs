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

        public virtual void Dispose()
        {
            Logger.Debug("Disposing service {Service}", GetType().Name);
        }

        public virtual ValueTask DisposeAsync()
        {
            Logger.Debug("Disposing service async {Service}", GetType().Name);
            return ValueTask.CompletedTask;
        }

        public virtual Task Init()
        {
            Logger.Debug("Initializing service {Service}", GetType().Name);
            return Task.CompletedTask;
        }

        public virtual Task Shutdown()
        {
            Logger.Debug("Shutdown service {Service}", GetType().Name);
            return Task.CompletedTask;
        }
    }
}
