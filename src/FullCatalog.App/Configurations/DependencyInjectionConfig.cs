using FullCatalog.App.Extensions;
using FullCatalog.Business.Interfaces;
using FullCatalog.Business.Notifications;
using FullCatalog.Business.Services;
using FullCatalog.Data;
using FullCatalog.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;

namespace FullCatalog.App.Configurations
{
    public static class DependencyInjectionConfig 
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            services.AddScoped<CatalogDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MonetaryValidationAttributeAdapterProvider>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
        
    }
}
