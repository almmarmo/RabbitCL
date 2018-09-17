using System;

namespace Rcl.Broker
{
    public interface IQueueSettings
    {
        TimeSpan TTL { get; set; }
        TimeSpan AutoExpire { get; set; }
        int MaxLength { get; set; }
        int MaxSizeBytes { get; set; }
        int MaxPriority { get; set; }
        string DeadLetterExchange { get; set; }
        string DeadLetterRoutingKey { get; set; }
    }
}
