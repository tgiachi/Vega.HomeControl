using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Interfaces.Entities;

namespace Vega.HomeControl.Api.Interfaces.Components
{
    public interface IVegaEmitterComponent
    {
        public Task<bool> Pool();
    }
}
