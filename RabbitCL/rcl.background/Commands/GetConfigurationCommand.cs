﻿using System;
using System.Collections.Generic;
using DocoptNet;
using Newtonsoft.Json;
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
            var config = _configurationIO.Get();
            var json = JsonConvert.SerializeObject(config, new Newtonsoft.Json.Converters.StringEnumConverter());
            Console.WriteLine(json);
        }
    }
}
