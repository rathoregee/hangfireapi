using Autofac;
using Autofac.Core;
using Hangfire;

namespace g2v.core.clinetsync.api
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            //builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            var container = builder.Build();

            //GlobalConfiguration.Configuration.UseSqlServerStorage("your connection string");
            GlobalConfiguration.Configuration.UseAutofacActivator(container);

        }
    }
}