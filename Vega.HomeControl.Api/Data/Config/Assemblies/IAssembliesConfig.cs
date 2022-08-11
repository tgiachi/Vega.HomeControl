using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Config.Net;

namespace Vega.HomeControl.Api.Data.Config.Assemblies
{
    public interface IAssembliesConfig
    {
        IEnumerable<string> AssembliesToLoad { get; }
    }
}
