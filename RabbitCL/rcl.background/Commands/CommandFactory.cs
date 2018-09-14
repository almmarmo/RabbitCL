using DocoptNet;
using rcl.Commands;
using Rcl.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rcl.background.Commands
{
    public static class CommandFactory
    {
        public static Command Create(IDictionary<string, ValueObject> arguments, IQueueService queueService)
        {
            if (arguments["--config"].IsTrue)
                return new GetConfigurationCommand();
            else if (arguments["configuration"].IsTrue)
                return new SetConfigurationCommand();
            else if (arguments["consume"].IsTrue)
                return new ConsumeQueueCommand(queueService);
            else if (arguments["updateenv"].IsTrue)
                return new UpdateEnvironmentCommand();
            else if (arguments["bindings"].IsTrue)
                return new BindingsCommand(queueService);

            return null;

        }
    }
}
