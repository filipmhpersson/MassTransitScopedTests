using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automatonymous;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Saga;
using MassTransitLab2.ConsumeExample;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MassTransitLab2
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _bus;

        public Worker(ILogger<Worker> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
            await SendNewSlip();
            await SendNewSlip();
            await SendNewSlip();
            await SendNewSlip();
            await SendNewSlip();
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:scoped"));
            await endpoint.Send<ITestEvent>(new { });
        }

        private async Task SendNewSlip()
        {
            var builder = new RoutingSlipBuilder(Guid.NewGuid());
            builder.AddActivity("ScopedActivity", new Uri("queue:scoped_execute"));

            await _bus.Execute(builder.Build());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bus.StopAsync(cancellationToken);
        }
    }
}