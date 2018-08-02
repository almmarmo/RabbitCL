using DocoptNet;
using System;
using rcl.Commands;
using System.Collections.Generic;

namespace rcl
{
    class Program
    {
        private const string usage = @"
RabbitCL
Usage:
    rcl configuration [--host=<host>] [--port=<port>] [--user=<user>] [--pass=<pass>] [--ssl=<ssl>]
    rcl get           --queue=<queue> --ack=<port> 
    rcl               (-h | --help)
    rcl --version
    rcl --config

Options:
    -h --help         Show this screen.
    --version         Show version.
    --config          Get current configuration settings.
    --host=HOST       Broker host connection
    --port=PORT       Broker port connection
    --user=USER       Broker username
    --pass=PASS       Broker password
    --ssl=SSL         Broker enable SSL
    --queue=QUEUE     Broker queue name
";

        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, version: "Rabbit CL 0.0.1", exit: true);


            //foreach (var argument in arguments)
            //{
            //    Console.WriteLine("{0} = {1}", argument.Key, argument.Value);
            //}

            var command = Factory(arguments);
            if (command != null)
            {
                Console.WriteLine(command.Name);
                command.Execute(arguments);
            }
        }

        private static Command Factory(IDictionary<string, ValueObject> arguments)
        {
            if (arguments["--config"].IsTrue)
                return new GetConfigurationCommand();
            else if (arguments["configuration"].IsTrue)
                return new SetConfigurationCommand();

            return null;
        }
    }
}
