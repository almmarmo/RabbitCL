using DocoptNet;
using System;
using rcl.Commands;
using System.Collections.Generic;
using rcl.Configurations;
using Autofac;
using rcl.Entities;
using rcl.IO;
using Rcl.Broker;
using System.Linq;
using rcl.background.Enums;
using Rcl.Broker.RabbitMQ.DependencyInjection;
using rcl.background.Commands;

namespace rcl
{
    class Program
    {
        private static Configuration _configuration { get => new ConfigurationIO().Get(); }
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var arguments = new Docopt().Apply(UsageConfiguration.USAGE, args, version: "Rabbit CL 0.0.1", exit: true);

            //foreach (var argument in arguments)
            //{
            //    Console.WriteLine("{0} = {1}", argument.Key, argument.Value);
            //}

            var config = _configuration;

            var builder = new ContainerBuilder();

            EnvironmentFactory(arguments, config, builder);

            Container = builder.Build();

            try
            {
                var command = CommandFactory.Create(arguments, Container.Resolve<IQueueService>());//  CommandFactory(arguments);
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
            catch (Autofac.Core.Registration.ComponentNotRegisteredException ex)
            {
                Console.WriteLine($"[ERROR] (COMPONENT NOT REGISTRED) => {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] (COMMAND FACTORY) => {ex.Message}");
            }
        }

        private static void EnvironmentFactory(IDictionary<string, ValueObject> arguments, Configuration config, ContainerBuilder builder)
        {
            if(config != null && arguments["--env"] != null)
            {
                var environmentArgument = arguments["--env"].Value.ToString();

                var environment = config.Environments.FirstOrDefault(x => x.Name == environmentArgument);
                if(environment != null)
                {
                    switch(environment.BrokerType)
                    {
                        case EBrokerType.RabbitMQ:
                            builder.AddRabbitMQ(options =>
                            {
                                options.Host = environment.Host;
                                options.Port = environment.Port;
                                options.EnableSsl = environment.Ssl;
                                options.Username  = environment.Username;
                                options.Password = environment.Password;
                            });
                            break;
                    }
                }
            }
        }
    }
}
