using DocoptNet;
using System.Collections.Generic;

namespace rcl.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(IDictionary<string, ValueObject> arguments);
    }
}
