using Microsoft.Extensions.DependencyInjection;
using Services.Services;

namespace Services.Configuration
{
    public static class DIServiceCollectionExtension
    {
        public static IServiceCollection AddLogicServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
