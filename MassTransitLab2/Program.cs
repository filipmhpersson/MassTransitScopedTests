using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Definition;
using MassTransitLab2.ConsumeExample;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MassTransitLab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IContextService, ContextService>();
                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddActivities(typeof(ScopedActivity));
                        cfg.AddConsumers(typeof(ScopedConsumer));
                        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

                        cfg.UsingRabbitMq((context, configurator) =>
                        {
                            configurator.UseServiceScope(context);
                            configurator.UseExecuteActivityFilter(typeof(AddActivityScopedValueFilter<>), context);
                            configurator.UseConsumeFilter(typeof(AddConsumerScopedValueFilter<>), context);
                            configurator.ConfigureEndpoints(context);
                        });
                    });

                    services.AddHostedService<Worker>();
                });
    }
}