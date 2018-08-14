using DocoptNet;
using System;
using rcl.Commands;
using System.Collections.Generic;
using rcl.Configurations;

namespace rcl
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(UsageConfiguration.USAGE, args, version: "Rabbit CL 0.0.1", exit: true);

            //foreach (var argument in arguments)
            //{
            //    Console.WriteLine("{0} = {1}", argument.Key, argument.Value);
            //}

            try
            {
                var command = Factory(arguments);
                try
                {
                    if (command != null)
                    {
                        //Console.WriteLine(command.Name);
                        command.Execute(arguments);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] ({command.Name}) => {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] (COMMAND FACTORY) => {ex.Message}");
            }
        }

        private static Command Factory(IDictionary<string, ValueObject> arguments)
        {
            if (arguments["--config"].IsTrue)
                return new GetConfigurationCommand();
            else if (arguments["configuration"].IsTrue)
                return new SetConfigurationCommand();
            else if (arguments["consume"].IsTrue)
                return new ConsumeQueueCommand();

            return null;
        }
    }
}
