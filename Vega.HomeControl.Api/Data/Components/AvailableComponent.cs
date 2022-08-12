using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Attributes.Components;

namespace Vega.HomeControl.Api.Data.Components
{
    public class AvailableComponent
    {
        public VegaComponentAttribute ComponentAttribute { get; set; }

        public Type ComponentType { get; set; }
    }
}
