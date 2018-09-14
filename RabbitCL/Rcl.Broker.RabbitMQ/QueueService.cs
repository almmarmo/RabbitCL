using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using Rcl.Broker.RabbitMQ.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Rcl.Broker.RabbitMQ
{
    public class QueueService : IQueueService
    {
        private readonly RabbitMQOptions _options;
        private readonly ConnectionFactory _factory;

        private IConnection _connection { get; set; }
        private IModel _requestChannel { get; set; }
        private IModel _publishChannel { get; set; }

        public QueueService(RabbitMQOptions options)
        {
            _options = options;

            var protocol = "amqp";

            var sslOption = new SslOption();
            if (_options.EnableSsl)
            {
                sslOption.Enabled = true;
                sslOption.ServerName = _options.Host;
                sslOption.AcceptablePolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch;

                protocol = "amqps";
            }

            _factory = new ConnectionFactory()
            {
                Ssl = sslOption,
                TopologyRecoveryEnabled = false,
                AutomaticRecoveryEnabled = true,
                Uri = new Uri($"{protocol}://{_options.Username}:{_options.Password}@{_options.Host}:{_options.Port}"),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            };
            _factory.Ssl.CertificateValidationCallback += (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

            _connection = _factory.CreateConnection();
            _requestChannel = _connection.CreateModel();
        }

        public void Acknowledge(QueueMessage message)
        {
            _requestChannel.BasicAck(message.Id, true);
        }

        public void Bind(string queue, string[] exchanges, string[] rountingKeys)
        {
            if (rountingKeys != null)
            {
                foreach (var exchange in exchanges)
                {
                    foreach (var key in rountingKeys)
                    {
                        try
                        {
                            _requestChannel.QueueBind(queue, exchange, key);
                        }
                        catch (OperationInterruptedException ex)
                        {
                            throw new Exception(ex.ShutdownReason.ReplyText);
                        }

                    }
                }
            }
        }

        public void Clean(string name)
        {
            throw new NotImplementedException();
        }

        public void Create(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<QueueMessage> Get(string queueName, QueueServiceGetOptions options = null)
        {
            var list = new List<QueueMessage>();
            while (true)
            {
                var message = _requestChannel.BasicGet(queueName, false);
                if (message != null)
                    list.Add(new QueueMessage(message.DeliveryTag, Encoding.UTF8.GetString(message.Body)));
                else
                    break;
            }
            return list;
        }

        public void Publish(string message, QueueServicePublishingOptions options = null)
        {
            using (var publishChannel = _connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);
                publishChannel.BasicPublish(exchange: options.ExchangeName, routingKey: options.RoutingKey, basicProperties: null, body: body);
            }
        }

        public void Purge(string name)
        {
            throw new NotImplementedException();
        }

        public void Unbind(string queue, string[] exchanges, string[] rountingKeys)
        {
            if (rountingKeys != null)
            {
                foreach (var exchange in exchanges)
                {
                    foreach (var key in rountingKeys)
                    {
                        try
                        {
                            _requestChannel.QueueUnbind(queue, exchange, key);
                        }
                        catch (OperationInterruptedException ex)
                        {
                            throw new Exception(ex.ShutdownReason.ReplyText);
                        }

                    }
                }
            }
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
