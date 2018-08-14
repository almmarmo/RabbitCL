using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DocoptNet;
using rcl.Entities;
using rcl.IO;

namespace rcl.Commands
{
    public class SetConfigurationCommand : Command
    {
        private readonly string _filePath;
        private readonly string _folderPath;
        private readonly ConfigurationIO _configurationIO;
        public SetConfigurationCommand()
        {
            _folderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _filePath = $"{_folderPath}\\config.json";

            _configurationIO = new ConfigurationIO();
        }

        public override string Name => "SET CONFIGURATION";

        public override void Execute(IDictionary<string, ValueObject> arguments)
        {
            var host = arguments["--host"] != null ? arguments["--host"].Value.ToString() : "";
            var port = arguments["--port"] != null ? arguments["--port"].Value.ToString() : "";
            var user = arguments["--user"] != null ? arguments["--user"].Value.ToString() : "";
            var pass = arguments["--pass"] != null ? arguments["--pass"].Value.ToString() : "";
            var ssl = arguments["--ssl"] != null ? arguments["--ssl"].Value.ToString() : "";

            var configuration = new Configuration(host, port, user,pass, ssl);

            _configurationIO.Write(configuration);
        }
    }
}
