using System;
using System.Collections.Generic;
using DocoptNet;
using rcl.IO;

namespace rcl.Commands
{
    public class GetConfigurationCommand : Command
    {
        private readonly ConfigurationIO _configurationIO;

        public GetConfigurationCommand()
        {
            _configurationIO = new ConfigurationIO();
        }

        public override string Name => "GET CONFIGURATION";

        public override void Execute(IDictionary<string, ValueObject> arguments)
        {
            Console.WriteLine(_configurationIO.Get());
        }
    }
}
