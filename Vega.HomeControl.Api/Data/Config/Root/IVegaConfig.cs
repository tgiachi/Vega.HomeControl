using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Data.Config.Assemblies;
using Vega.HomeControl.Api.Data.Config.Common;
using Vega.HomeControl.Api.Data.Config.Components;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Data.Config.Root
{
    public interface IVegaConfig
    {
        IVegaCommonConfig Common { get; }

        IAssembliesConfig Assemblies { get; set; }

        IVegaService Services { get; set; }

        IVegaComponentsConfig Components { get; set; }
    }
}
