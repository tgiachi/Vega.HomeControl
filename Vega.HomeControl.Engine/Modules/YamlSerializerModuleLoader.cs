using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Vega.HomeControl.Api.Attributes.Core;
using Vega.HomeControl.Api.Impl.Modules;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vega.HomeControl.Engine.Modules
{
    [VegaModuleLoader]
    internal class YamlSerializerModuleLoader : VegaModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new SerializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build()).As<ISerializer>();

            builder.RegisterInstance(new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build()).As<IDeserializer>();

            base.Load(builder);
        }

        public YamlSerializerModuleLoader(ILogger logger) : base(logger)
        {
        }
    }
}
