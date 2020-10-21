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
        private readonly IContextService _contextService;
        public ScopedActivity(IContextService contextService)
        {
            _contextService = contextService;
        }

        public Task<ExecutionResult> Execute(ExecuteContext<Arguments> context)
        {
            Console.WriteLine($"Activity TenantId: {_contextService.TenantId}");
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