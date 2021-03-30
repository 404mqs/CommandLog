using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CommandLog
{ 
    public class Config : IRocketPluginConfiguration
    {
        public string DiscordWebHookLink;
        [XmlArrayItem(ElementName = "command")]
        public List<CommandLog> LogCommands;

        public void LoadDefaults()
        {
            DiscordWebHookLink = "Discord Webhook Here";
         
            LogCommands = new List<CommandLog>
            {
                new CommandLog { name = "i" },
                new CommandLog { name = "v" },
            };
        }
    }
}
