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
        //    builder.RegisterType<EventBusService>().As<IEventBusService>().SingleInstance();

            base.Load(builder);
        }
    }
}
