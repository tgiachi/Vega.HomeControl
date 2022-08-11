using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Attributes.Core
{
    [AttributeUsage(AttributeTargets.Class)]
    public class VegaModuleLoaderAttribute : Attribute
    {
        public VegaModuleLoaderAttribute()
        {

        }
    }
}
