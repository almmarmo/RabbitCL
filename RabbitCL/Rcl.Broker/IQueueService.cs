using System;
using System.Collections.Generic;

namespace Rcl.Broker
{
    public interface IQueueService : IDisposable
    {
        ICollection<QueueMessage> Get(string queueName, QueueServiceGetOptions options = null);
        void Publish(string message, QueueServicePublishingOptions options = null);
        void Acknowledge(QueueMessage message);
        void Create(string name);
        void Purge(string name);
        void Clean(string name);
        void Bind(string queue, string[] exchanges, string[] rountingKeys);
        void Unbind(string queue, string[] exchanges, string[] rountingKeys);
    }
}
