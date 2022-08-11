using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using Serilog;
using Vega.HomeControl.Api.Impl.Services;
using Vega.HomeControl.Api.Interfaces.Services;

namespace Vega.HomeControl.Engine.Services
{
    public class WebServerService : AbstractVegaEventService, IWebServerService
    {
        private WebServer _webServer;

        public WebServerService(ILogger logger, IEventBusService eventBusService) : base(logger, eventBusService)
        {
            Init();
        }

        private void Init()
        {
            _webServer = new WebServer(o => {
                o
                    .WithUrlPrefix("http://0.0.0.0:8989")
                    .WithMode(HttpListenerMode.EmbedIO);
            });

            _webServer.StateChanged += (sender, args) => {
                Logger.Information("WebServer status: {State}", args.NewState);
            };
        }

        public Task Start()
        {
            return _webServer.RunAsync();
        }

        public override Task Shutdown()
        {
            _webServer.Dispose();
            return base.Shutdown();
        }
    }
}
