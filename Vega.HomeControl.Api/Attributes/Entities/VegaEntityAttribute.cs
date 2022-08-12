using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.HomeControl.Api.Attributes.Entities
{

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class VegaEntityAttribute : Attribute
    {
        public string CollectionName { get; set; }

        public VegaEntityAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
