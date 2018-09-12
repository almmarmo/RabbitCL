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
                throw new Exception("ENVIRONMENT NAME ALREADY EXISTS.");

            this._environments.Add(environment);
        }

        public void UpdateEnvironment(rcl.background.Entities.Environment environment)
        {
            var env = _environments.FirstOrDefault(x => x.Name == environment.Name);
            if (env != null)
            {
                _environments.Remove(env);
                _environments.Add(environment);
            }
            else
                throw new Exception("ENVIRONMENT NOT EXIST.");
        }

        public void RemoveEnvironment(string name)
        {
            var env = _environments.FirstOrDefault(x => x.Name == name);
            if (env != null)
                _environments.Remove(env);
        }

        public rcl.background.Entities.Environment GetEnvironment(string name)
        {
            return _environments.FirstOrDefault(x => x.Name == name);
        }

        public override string ToString()
            => JsonConvert.SerializeObject(this);
    }
}
