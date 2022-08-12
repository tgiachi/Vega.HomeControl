using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Attributes.Components;
using Vega.HomeControl.Api.Interfaces.Components;
using Vega.HomeControl.Components.Test.Entities;

namespace Vega.HomeControl.Components.Test
{

    [VegaComponent("test_component", "Test component", "1.0", "TEST", typeof(void))]
    public class TestComponent : IVegaComponent, IVegaEmitterComponent
    {
        public Task<object> DefaultConfig()
        {
            throw new NotImplementedException();
        }

        public Task InitConfig(object config)
        {
            throw new NotImplementedException();
        }

        public Task<object> SaveConfig()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        [EmitterDelay(30)]
        public Task<bool> Pool()
        {
            return Task.FromResult(true);
        }
    }
}
