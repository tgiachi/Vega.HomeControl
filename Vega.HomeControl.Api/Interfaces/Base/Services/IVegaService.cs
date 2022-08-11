using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Interfaces.Base.Services
{
    public interface IVegaService : IDisposable, IAsyncDisposable
    {
        Task Shutdown();
    }
}
