using DocoptNet;
using Newtonsoft.Json;
using rcl.Entities;
using rcl.IO;
using rcl.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace rcl.Commands
{
    public class ConsumeQueueCommand : Command
    {
        private readonly QueueService _queueService;
        private readonly Configuration _configuration;

        public ConsumeQueueCommand()
        {
            _configuration = new ConfigurationIO().Get();
            _queueService = new QueueService(_configuration);
        }

        public override string Name => "CONSUME QUEUE CONFIGURATION";

        public override void Execute(IDictionary<string, ValueObject> arguments)
        {
            var queue = arguments["--queue"] != null ? arguments["--queue"].Value.ToString() : "";
            var ack = arguments["--ack"] != null ? arguments["--ack"].Value.ToString() : "";
            var output = arguments["--out"] != null ? arguments["--out"].Value.ToString() : "";

            string folder = output == "." ? Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) : output;
            if (!Directory.Exists(folder))
                throw new Exception("OUTPUT PATH DOES NOT EXISTIS");

            var messages = _queueService.GetMessages(queue);

            foreach(var message in messages)
            {
                if (ack == "true")
                    _queueService.Acknowledge(message.DeliveryTag);

                string file = $"{folder}\\{DateTime.Now.ToString("yyyyMMddHHmmss.fff")}.txt";

                File.WriteAllText(file, message.Payload);
            }

            _queueService.Dispose();
        }
    }
}
