using Newtonsoft.Json;
using rcl.Entities;
using System.IO;
using System.Reflection;

namespace rcl.IO
{
    public class ConfigurationIO
    {
        private readonly string _filePath;
        private readonly string _folderPath;

        public ConfigurationIO()
        {
            _folderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _filePath = $"{_folderPath}\\config.json";
        }

        public void Write(Configuration configuration)
        {
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(configuration));
        }

        public Configuration Get()
        {
            if (File.Exists(_filePath))
            {
                var line = File.ReadAllLines(_filePath)[0];
                return JsonConvert.DeserializeObject<Configuration>(line);
            }
            return null;
        }
    }
}
