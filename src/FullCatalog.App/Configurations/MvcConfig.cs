using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FullCatalog.App.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
