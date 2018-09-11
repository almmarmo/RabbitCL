using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rcl.Entities
{
    public class Configuration
    {
        public Configuration()
        {
            _environments = new List<background.Entities.Environment>();
        }

        private List<rcl.background.Entities.Environment> _environments;
        public IEnumerable<rcl.background.Entities.Environment> Environments { get => _environments; }

        public void AddEnvironment(rcl.background.Entities.Environment environment)
        {
            if (this.Environments.Any(x => x.Name == environment.Name))
                throw new Exception("ENVIRONMENT NAME ALREADY EXISTIS.");

            this._environments.Add(environment);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
