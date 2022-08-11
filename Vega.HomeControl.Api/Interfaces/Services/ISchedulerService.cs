using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Interfaces.Services
{
    public interface ISchedulerService : IVegaService
    {
        void AddJob(string name, Action action, int seconds, bool runOnce);

        void RemoveJob(string name);
    }
}
