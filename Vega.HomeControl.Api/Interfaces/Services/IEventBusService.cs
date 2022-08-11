using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.HomeControl.Api.Impl.Events;
using Vega.HomeControl.Api.Interfaces.Base.Services;

namespace Vega.HomeControl.Api.Interfaces.Services
{
    public interface IEventBusService : IVegaService
    {
        Task PublishNotification<TEvent>(IVegaNotificationEvent<TEvent> @event);
        Task PublishRequest<TEvent>(IVegaRequestEvent<TEvent> @event);
    }
}
