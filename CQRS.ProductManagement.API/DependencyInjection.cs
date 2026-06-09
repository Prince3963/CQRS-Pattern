using CQRS.ProductManagement.Application.Behaviors;
using CQRS.ProductManagement.Application.Features.Products.Commands.CreateProduct;
using CQRS.ProductManagement.Application.Features.Products.Commands.UpdateProduct;
using CQRS.ProductManagement.Application.Interfaces;
using CQRS.ProductManagement.Persistence.Context;
using CQRS.ProductManagement.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.ProductManagement.API;

public static class DependencyInjection
{
    public static IServiceCollection AddProductManagementServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();
        services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductValidator>();

        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("defaultConnection"));
        });

        return services;
    }
}
