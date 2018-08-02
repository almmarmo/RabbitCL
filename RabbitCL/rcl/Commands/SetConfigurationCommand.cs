using System;
using System.Collections.Generic;
using DocoptNet;

namespace rcl.Commands
{
    public class SetConfigurationCommand : Command
    {
        public override string Name => "SET CONFIGURATION";

        public override void Execute(IDictionary<string, ValueObject> arguments)
        {
            Console.WriteLine("EXECUTE SetConfigurationCommand");
        }
    }
}
