using GreenPipes;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitLab2.ConsumeExample
{
    public class AddConsumerScopedValueFilter<T> :
        IFilter<ConsumeContext<T>>
        where T : class
    {
        private readonly IContextService _contextService;

        public AddConsumerScopedValueFilter(IContextService contextService)
        {
            _contextService = contextService;
        }

        public void Probe(ProbeContext context)
        {
            context.CreateFilterScope("scope");
        }

        public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
        {
            Console.WriteLine("Setting tenant id in consumer filter");
            _contextService.TenantId = "Hello";

            await next.Send(context);
        }
    }
}