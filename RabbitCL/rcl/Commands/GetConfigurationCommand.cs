using System;
using System.Collections.Generic;
using DocoptNet;

namespace rcl.Commands
{
    public class GetConfigurationCommand : Command
    {
        public override string Name => "GET CONFIGURATION";

        public override void Execute(IDictionary<string, ValueObject> arguments)
        {
            Console.WriteLine("EXECUTE GetConfigurationCommand");
        }
    }
}
