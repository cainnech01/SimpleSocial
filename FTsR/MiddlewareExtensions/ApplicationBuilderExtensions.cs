using FTsR.SubscribeTableDependencies;

namespace FTsR.MiddlewareExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseProductTableDependency(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            service.SubscribeTableDependency();
        }

        public static void UseCompaniesTableDependency(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeCompaniesTableDependency>();
            service.SubscribeTableDependency();
        }

        public static void UsePinsTableDependency(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<SubscribePinsTableDependency>();
            service.SubscribeTableDependency();
        }
    }
}

