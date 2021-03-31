using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace CommandLog
{
    public class MQSPlugin : RocketPlugin<Config>
    {
        public static MQSPlugin Instance;
        protected override void Load()
        {
            Instance = this;
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            Logger.LogWarning($"[{Name}] has been loaded! ");
            Logger.LogWarning("Dev: MQS#7816");
            Logger.LogWarning("Join this Discord for Support: https://discord.gg/Ssbpd9cvgp");
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            ChatManager.onChatted += onChat;

        }

        public static void SendToDiscord(string caller, string displayname, string command, string text)
        {
            var discordWebHookLink = MQSPlugin.Instance.Configuration.Instance.DiscordWebHookLink;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(discordWebHookLink);
            var f = "{ \"content\": \"`" + displayname + " [" + caller + "] | COMMAND: " + command + "`" +"\"}";
            var bytes = Encoding.ASCII.GetBytes(f);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = (long)bytes.Length;
            using (var requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }
            httpWebRequest.GetResponse();
        }

        private void onChat(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool isRich, string text, ref bool isVisible)
        {
            var converted = UnturnedPlayer.FromSteamPlayer(player);

            var webhook = Configuration.Instance.DiscordWebHookLink;

            var lower = text.ToLower();

            if (Configuration.Instance.LogCommands.Any(w => lower.StartsWith($"/{w.name} ")))
            { 
              if (webhook == "Discord Webhook Here")
              {
                return;
              }            
             
              SendToDiscord(Convert.ToString(converted.CSteamID), converted.DisplayName, text, webhook);

            }
        }

        protected override void Unload()
        {
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            Logger.LogWarning($"[{Name}] has been unloaded! ");
            Logger.LogWarning("++++++++++++++++++++++++++++++++++++++");
            ChatManager.onChatted -= onChat;
        }
    }
}