using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Impl.Events;

namespace Vega.HomeControl.Api.Data.Events.Impl
{
    public class VegaRequestEvent<TEvent> : IVegaRequestEvent<TEvent>
    {
        public string Sender { get; set; }
        public TEvent Data { get; set; }
        public Guid EventId { get; set; }

        public VegaRequestEvent(string sender, TEvent data)
        {
            Sender = sender;
            Data = data;
            EventId = Guid.NewGuid();
        }
    }
}
