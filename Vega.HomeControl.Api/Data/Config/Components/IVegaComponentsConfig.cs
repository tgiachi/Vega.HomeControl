using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Data.Config.Components
{
    public interface IVegaComponentsConfig
    {
        List<IVegaComponentLoaderConfig> Components { get; }
    }

    public interface IVegaComponentLoaderConfig
    {
        string Name { get; set; }

        bool IsEnabled { get; set; }
    }

}
