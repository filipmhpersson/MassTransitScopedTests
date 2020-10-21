using GreenPipes;
using MassTransit.Courier;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitLab2
{
    public class AddActivityScopedValueFilter<TArguments> :
        IFilter<ExecuteContext<TArguments>>
        where TArguments : class
    {
        private readonly IContextService _contextService;

        public AddActivityScopedValueFilter(IContextService contextService)
        {
            _contextService = contextService;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("scope");
        }

        public async Task Send(ExecuteContext<TArguments> context, IPipe<ExecuteContext<TArguments>> next)
        {
            Console.WriteLine("Setting activity tenant in filter");
            _contextService.TenantId = "Hello";

            await next.Send(context);
        }
    }
}