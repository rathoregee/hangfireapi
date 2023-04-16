using Autofac;
using Hangfire;
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