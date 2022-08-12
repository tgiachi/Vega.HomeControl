using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Attributes.Components
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class VegaComponentAttribute : Attribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string Category { get; set; }

        public Type ConfigType { get; set; }

        public VegaComponentAttribute(string name, string description, string version, string category, Type configType)
        {
            Name = name;
            Description = description;
            Version = version;
            Category = category;
            ConfigType = configType;
        }
    }
}
