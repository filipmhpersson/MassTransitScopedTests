using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitLab2.ConsumeExample
{
    public class ScopedConsumer : IConsumer<ITestEvent>
    {
        private readonly IContextService _contextService;

        public ScopedConsumer(IContextService contextService)
        {
            _contextService = contextService;
        }

        public Task Consume(ConsumeContext<ITestEvent> context)
        {
            Console.WriteLine($"Consumer tentant: {_contextService.TenantId}");
            return Task.FromResult(context.ConsumeCompleted);
        }
    }

    public interface ITestEvent
    {
    }
}