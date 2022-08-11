using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Vega.HomeControl.Api.Attributes.Core;
using Vega.HomeControl.Api.Interfaces.Services;
using Vega.HomeControl.Engine.Services;

namespace Vega.HomeControl.Engine.Modules
{

    [VegaModuleLoader]
    public class DefaultServiceModuleLoader : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScriptEngineService>().As<IScriptEngineService>().SingleInstance();
            builder.RegisterType<SchedulerService>().As<ISchedulerService>().SingleInstance().AutoActivate();
            builder.RegisterType<WebServerService>().As<IWebServerService>().SingleInstance();
            builder.RegisterType<VaultService>().As<IVaultService>().SingleInstance();

            base.Load(builder);
        }
    }
}
