using rcl.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using rcl.Abstractions;
using Newtonsoft.Json;

namespace rcl.Services
{
    public class QueueService : IDisposable
    {
        private readonly Configuration _configuration;
        private readonly ConnectionFactory _factory;

        private IConnection _connection { get; set; }
        private IModel _requestChannel { get; set; }
        private IModel _publishChannel { get; set; }


        public QueueService(Configuration configuration)
        {
            _configuration = configuration;

            var protocol = "amqp";

            var sslOption = new SslOption();
            if (_configuration.Ssl)
            {
                sslOption.Enabled = true;
                sslOption.ServerName = _configuration.Host;
                sslOption.AcceptablePolicyErrors |= System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch;

                protocol = "amqps";
            }

            _factory = new ConnectionFactory()
            {
                Ssl = sslOption,
                TopologyRecoveryEnabled = false,
                AutomaticRecoveryEnabled = true,
                Uri = new Uri($"{protocol}://{_configuration.Username}:{_configuration.Password}@{_configuration.Host}:{_configuration.Port}"),
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            };
            _factory.Ssl.CertificateValidationCallback += (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

            _connection = _factory.CreateConnection();
            _requestChannel = _connection.CreateModel();
        }

        public void Acknowledge(ulong messageId)
        {
            _requestChannel.BasicAck(messageId, true);
        }

        public void Publish(string payload, string exchange, string queue)
        {
            using (var publishChannel = _connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(payload);
                publishChannel.BasicPublish(exchange: exchange, routingKey: queue, basicProperties: null, body: body);
            }
        }

        public ICollection<QueueMessage<string>> GetMessages(string queue)
        {
            var list = new List<QueueMessage<string>>();
            while (true)
            {
                var message = _requestChannel.BasicGet(queue, false);
                if (message != null)
                    list.Add(new QueueMessage<string>(message.DeliveryTag, Encoding.UTF8.GetString(message.Body)));
                else
                    break;
            }
            return list;
        }

        public ICollection<QueueMessage<T>> GetMessages<T>(string queue)
        {
            var list = new List<QueueMessage<T>>();

            foreach (var message in GetMessages(queue))
                list.Add(new QueueMessage<T>(message.DeliveryTag, JsonConvert.DeserializeObject<T>(message.Payload)));

            return list;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
