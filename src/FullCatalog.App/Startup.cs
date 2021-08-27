using FullCatalog.App.Data;
using FullCatalog.App.Extensions;
using FullCatalog.Business.Interfaces;
using FullCatalog.Data;
using FullCatalog.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;

namespace FullCatalog.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FullCatalogConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("FullCatalogConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "The given value to this field is invalid.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "This field requires a value.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "This field requires a value.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "The request body cannot be null.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => "The given value to this field is invalid.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "The given value to this field is invalid.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "The field must be a number.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => "The given value to this field is invalid.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => "The given value to this field is invalid.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "The field must be a number.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "This field requires a value.");
                o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddScoped<CatalogDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MonetaryValidationAttributeAdapterProvider>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture},
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
