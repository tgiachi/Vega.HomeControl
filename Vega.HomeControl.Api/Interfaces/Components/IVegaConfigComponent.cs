using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Interfaces.Components
{
    public interface IVegaConfigComponent
    {
        Task<object> DefaultConfig();

        Task InitConfig(object config);

        Task<object> SaveConfig();
    }
}
