using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;
using Vega.HomeControl.Api.Impl.Events;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class EventBusService : AbstractBaseVegaService, IEventBusService
    {
        private readonly IMediator _mediator;
        public EventBusService(ILogger logger, IMediator mediator) : base(logger)
        {
            _mediator = mediator;
        }

        public Task PublishNotification<TEvent>(IVegaNotificationEvent<TEvent> @event)
        {
            Logger.Debug("Received notification from {Sender} type of event: {Event}", @event.Sender, @event.Data!.GetType().Name);
            return _mediator.Publish(@event);
        }

        public Task PublishRequest<TEvent>(IVegaRequestEvent<TEvent> @event)
        {
            Logger.Debug("Received notification from {Sender} type of event: {Event}", @event.Sender, @event.Data!.GetType().Name);
            return _mediator.Send(@event);
        }
    }
}
