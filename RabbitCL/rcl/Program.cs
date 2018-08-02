using DocoptNet;
using System;

namespace rcl
{
    class Program
    {
        private const string usage = @"
RabbitCL
Usage:
    rcl (set|get) configuration [--host=<host>] [--port=<port>] [--user=<user>] [--pass=<pass>] [--ssl=<port>]
    rcl (-h | --help)
    rcl --version
    rcl --config

Options:
    set             Setting values.
    get             Getting value.
    configuration   Configuration properties.
    -h --help       Show this screen.
    --version       Show version.
    --config        Get current configuration settings.
    --drifting      Drifting mine.
    --host=HOST     Broker host connection [default: localhost]    
    --port=PORT     Broker port connection [default: 5672]
    --user=USER     Broker username [default: guest]
    --pass=PASS     Broker password [default: guest]
    --ssl=SSL       Broker enable SSL [default: false]

";

        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(usage, args, version: "Rabbit CL 0.0.1", exit: true);

            foreach (var argument in arguments)
            {
                switch(argument.Key)
                {
                    //case "--config":
                    //    Console.WriteLine("get default configuration {0} = {1}", argument.Key, argument.Value);
                    //    break;
                    default:
                        Console.WriteLine("{0} = {1}", argument.Key, argument.Value);
                        break;
                }
            }
        }
    }
}
