﻿namespace Rcl.Broker.RabbitMQ.DependencyInjection
{
    public class RabbitMQOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}