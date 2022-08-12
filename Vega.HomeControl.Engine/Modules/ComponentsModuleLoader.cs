using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Vega.HomeControl.Api.Attributes.Components;
using Vega.HomeControl.Api.Attributes.Core;
using Vega.HomeControl.Api.Data.Components;
using Vega.HomeControl.Api.Impl.Modules;
using Vega.HomeControl.Api.Utils;

namespace Vega.HomeControl.Engine.Modules
{

    [VegaModuleLoader]
    public class ComponentsModuleLoader : VegaModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            Logger.Information("Scanning for modules");
            var availableComponents = new List<AvailableComponent>();

            foreach (var componentType in AssemblyUtils.GetAttribute<VegaComponentAttribute>())
            {
                var attribute = componentType.GetCustomAttribute<VegaComponentAttribute>();
                Logger.Information("Component [{Category} - {Name} v{Version}]", attribute.Category, attribute.Name, attribute.Version);
                builder.RegisterType(componentType).SingleInstance();
                availableComponents.Add(new AvailableComponent { ComponentAttribute = attribute, ComponentType = componentType });
            }

            builder.RegisterInstance(availableComponents);

            base.Load(builder);

        }

        public ComponentsModuleLoader(ILogger logger) : base(logger)
        {
        }
    }
}
