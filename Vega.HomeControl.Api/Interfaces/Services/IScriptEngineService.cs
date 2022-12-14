using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Interfaces.Services
{
    public interface IScriptEngineService : IVegaService
    {
        Task LoadString(string value);
        Task LoadFile(string value);

    }
}
