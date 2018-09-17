﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Rcl.Broker
{
    public class CreateQueueSettings
    {
        public TimeSpan TTL { get; set; }
        public TimeSpan AutoExpire { get; set; }
        public int MaxLength { get; set; }
        public int MaxSizeBytes { get; set; }
        public int MaxPriority { get; set; }
        public string DeadLetterExchange { get; set; }
        public string DeadLetterRoutingKey { get; set; }
    }
}
