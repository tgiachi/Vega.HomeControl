using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Vega.HomeControl.Api.Impl.Events
{
    public interface IVegaNotificationEvent<TEvent> : INotification
    {
        string Sender { get; set; }

        TEvent Data { get; set; }

        Guid EventId { get; set; }
    }
}
