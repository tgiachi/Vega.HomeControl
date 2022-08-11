using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Vega.HomeControl.Api.Data.Events;
using Vega.HomeControl.Api.Data.Events.Impl;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Api.Impl.Services
{
    public class AbstractVegaEventService : AbstractBaseVegaService
    {
        private readonly IEventBusService _eventBusService;

        public AbstractVegaEventService(ILogger logger, IEventBusService eventBusService) : base(logger)
        {
            _eventBusService = eventBusService;
        }

        protected Task PublishNotification<TEvent>(TEvent @event)
        {
            return _eventBusService.PublishNotification(new VegaNotificationEvent<TEvent>(GetType().FullName!, @event));
        }

        protected Task PublishRequest<TEvent>(TEvent @event)
        {
            return _eventBusService.PublishRequest(new VegaRequestEvent<TEvent>(GetType().FullName!, @event));
        }
    }
}
