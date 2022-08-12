using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using Serilog;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class SchedulerService : AbstractVegaEventService, ISchedulerService
    {
        public SchedulerService(ILogger logger, IEventBusService eventBusService) : base(logger, eventBusService)
        {
            Logger.Information("Starting Job Scheduler service");
            JobManager.Start();
        }

        public void AddJob(string name, Action action, int seconds, bool runOnce)
        {
            Logger.Debug("Adding scheduled job: {Name} every {Seconds} seconds, runOne: {RunOnce}", name, seconds, runOnce);
            JobManager.AddJob(action, schedule => schedule.WithName(name).ToRunEvery(seconds).Seconds());
        }

        public void RemoveJob(string name)
        {
            JobManager.RemoveJob(name);
        }

        public override Task Shutdown()
        {
            JobManager.StopAndBlock();
            return base.Shutdown();
        }
    }
}
