using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MultiShop.Mvc.DataAccess.Infrastructure.IRepository;
using MultiShop.Mvc.DataAccess.Infrastructure.Repository;
using MultiShop.Mvc.DataAccess.ServiceBus.EmailService;
using MultiShop.Mvc.Utills;
using System.Net.Http;

namespace MultiShop.MVC.IOC
{
    public class AutofacSetup
    {
        public static IContainer GetServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            //Register Services Here
            builder.RegisterType<OrderConsumeApi>().As<IOrderConsumeApi>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSending>().As<IEmailSending>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryConsumeApi>().As<ICategoryConsumeApi>().InstancePerLifetimeScope();
            builder.RegisterType<UserAccount>().As<IUserAccount>().InstancePerLifetimeScope();
            builder.RegisterType<Products>().As<IProductConsumeApi>().InstancePerLifetimeScope();
            builder.RegisterType<HttpClient>().As<HttpClient>().InstancePerLifetimeScope();
            builder.RegisterType<CartConsumeApi>().As<ICartConsumeApi>().InstancePerLifetimeScope();
            builder.RegisterType<OrderDetailsConsumeApi>().As<IOrderDetailsConsuumeApi>().InstancePerLifetimeScope();
            builder.RegisterType<ApiCall>().As<IApiCall>().InstancePerLifetimeScope();
            builder.Populate(services);
            var container = builder.Build();
            return container;
        }
    }
}
