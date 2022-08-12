using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Attributes.Entities;
using Vega.HomeControl.Api.Impl.Entities;

namespace Vega.HomeControl.Components.Test.Entities
{

    [VegaEntity("tests")]
    public class TestEntity :BaseVegaEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
