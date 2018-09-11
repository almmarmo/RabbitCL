﻿using Autofac;
using System;

namespace Rcl.Broker.RabbitMQ.DependencyInjection
{
    public static class QueueServiceBrokerRabbitMQExtensions
    {
        public static void AddRabbitMQ(this ContainerBuilder builder, Action<RabbitMQOptions> configurations)
        {
            var options = new RabbitMQOptions();
            configurations.Invoke(options);

            builder.Register(c =>
            {
                return new QueueService(options);
            }).As<IQueueService>();
        }
    }
}
