using MassTransit.Courier;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitLab2
{
    public class ScopedActivity
   : IActivity<Arguments, Log>
    {
        public ScopedActivity()
        {
        }

        public Task<ExecutionResult> Execute(ExecuteContext<Arguments> context)
        {
            var success = context.TryGetPayload<IServiceProvider>(out var sp);
            var service = sp.GetRequiredService<IContextService>();
            Console.WriteLine($"Activity TenantId: {service.TenantId}");
            return Task.FromResult(context.Completed());
        }

        public Task<CompensationResult> Compensate(CompensateContext<Log> context)
        {
            Console.WriteLine("WE BLEW UP!!");
            return Task.FromResult(context.Compensated());
        }
    }

    public interface Log
    {
    }

    public interface Arguments
    {
    }
}