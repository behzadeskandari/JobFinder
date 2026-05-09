using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Behaviors;
using DinkToPdf;
using DinkToPdf.Contracts;
using FluentResults;
using FluentValidation;
using FluentValidation.AspNetCore;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.Customers.Command;
using JobFinder.Application.Feature.Customers.Handlers.CreateCutomerHandler;
using JobFinder.Application.Feature.Inventory.Command.UpdateInventoryCommand;
using JobFinder.Application.Feature.Inventory.Handlers.GetCurrentInventoryHandler;
using JobFinder.Application.Feature.Inventory.Handlers.GetSnapshotHistoryHandler;
using JobFinder.Application.Feature.Inventory.Handlers.UpdateInventoryHandler;
using JobFinder.Application.Feature.Inventory.Query.GetCurrentInventoryQuery;
using JobFinder.Application.Feature.Inventory.Query.GetSnapshotHistoryQuery;
using JobFinder.Application.Services;
using JobFinder.Contracts.Dtos.Product;
using JobFinder.Domain.Common.Entities;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddScoped<IMBTICalculationService, MBTICalculationService>();
            services.AddScoped<IDropDownServices, DropDownServices>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IPdfService, PdfService>();
            // Register FluentResult for all handlers
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));

            // Register all handlers with FluentResult integration
            services.AddTransient(typeof(IRequestHandler<,>), typeof(LoggingBehavior<,>));
            ///Customer
            services.AddTransient<IRequestHandler<CreateCustomerCommand, Result<Customer>>, CreateCustomerHandler>();
            //services.AddTransient<IRequestHandler<GetCustomersQuery, Result<List<CustomerDto>>>, GetCustomersHandler>();
            //services.AddTransient<IRequestHandler<DeleteCustomerCommand, Result<bool>>, DeleteCustomerHandler>();
            // Inventory handlers
            services.AddTransient<IRequestHandler<GetCurrentInventoryQuery, Result<List<ProductInventoryDto>>>, GetCurrentInventoryHandler>();
            services.AddTransient<IRequestHandler<UpdateInventoryCommand, Result<ProductInventoryDto>>, UpdateInventoryHandler>();
            services.AddTransient<IRequestHandler<GetSnapshotHistoryQuery, Result<SnapshotResponse>>, GetSnapshotHistoryHandler>();

            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Debug);
                builder.AddDebug();

            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ApplicationServiceRegistration).GetTypeInfo().Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            });
            services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).GetTypeInfo().Assembly);
            services.AddFluentValidationAutoValidation();


            return services;
        }

    }
}
