using Autofac;
using Autofac.Core;
using AutofacSerilogIntegration;
using Hangfire;
using Serilog;
namespace g2v.core.clinetsync.api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<BackgroundJobClient>().AsImplementedInterfaces();
            
        }
    }
}