using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.App.Products.CreateProduct;

namespace ProductManagement.App;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly); 


        
        return services;
    }
}