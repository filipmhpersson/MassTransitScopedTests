using System;
using System.Collections.Generic;
using System.Text;

namespace MassTransitLab2
{
    public class ContextService : IContextService
    {
        public string TenantId { get; set; }
    }

    public interface IContextService
    {
        string TenantId { get; set; }
    }
}