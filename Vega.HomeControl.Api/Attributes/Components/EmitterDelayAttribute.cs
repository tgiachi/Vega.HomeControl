using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace Vega.HomeControl.Api.Attributes.Components
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class EmitterDelayAttribute : Attribute
    {
        public long Interval { get; set; }
        public EmitterDelayAttribute(long interval)
        {
            Interval = interval;
        }
    }
}
