using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.IO;
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

        public static void sendDiscordWebhook(string URL, string escapedjson)
        {
            var wr = WebRequest.Create(URL);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
                sw.Write(escapedjson);
            wr.GetResponse();
        }

        private void onChat(SteamPlayer player, EChatMode mode, ref Color chatted, ref bool isRich, string text, ref bool isVisible)
        {
            var converted = UnturnedPlayer.FromSteamPlayer(player);

            var webhook = Configuration.Instance.DiscordWebHookLink;

            var lower = text.ToLower();

            var ip = SteamGameServer.GetPublicIP();

            var address = ip.ToString();

            if (Configuration.Instance.LogCommands.Any(w => lower.StartsWith($"/{w.name} ")))
            { 
                if (webhook == "Discord Webhook Here")
                {
                    return;
                }
                sendDiscordWebhook(webhook, ("{  \"username\": \"Command Log\",  \"avatar_url\": \"https://i.imgur.com/4M34hi2.png\",  \"embeds\": [    {      \"title\": \"Command Executed\",      \"color\": 15258703,      \"fields\": [        {          \"name\": \"Executer - !executer!\",          \"value\": \"ExecuterID\",          \"inline\": true        },        {          \"name\": \"Command:\",          \"value\": \"*!args!*\"        },        {          \"name\": \"Server IP\",          \"value\": \"!ip!\"        }      ],      \"thumbnail\": {        \"url\": \"https://i.pinimg.com/originals/1b/e8/41/1be84116e72d71bd6785c7050fefd2e3.gif\"      },      \"image\": {        \"url\": \"https://upload.wikimedia.org/wikipedia/commons/5/5a/A_picture_from_China_every_day_108.jpg\"      },      \"footer\": {        \"text\": \"CommandLog by ExoPlugins\",        \"icon_url\": \"https://media.discordapp.net/attachments/811275497966665739/823878043906080778/image0.png?width=128&height=128\"      }    }  ]}").Replace("!executer!", converted.DisplayName).Replace("*!args!*", $"`{lower}`").Replace("!ip!", address).Replace("ExecuterID", converted.Id));
            }          

            else if (Configuration.Instance.LogCommands.Any(w => lower.StartsWith($"/{w.name}")))
            {
                if (webhook == "Discord Webhook Here")
                {
                    return;
                }
                sendDiscordWebhook(webhook, ("{  \"username\": \"Command Log\",  \"avatar_url\": \"https://i.imgur.com/4M34hi2.png\",  \"embeds\": [    {      \"title\": \"Command Executed\",      \"color\": 15258703,      \"fields\": [        {          \"name\": \"Executer - !executer!\",          \"value\": \"ExecuterID\",          \"inline\": true        },        {          \"name\": \"Command:\",          \"value\": \"*!args!*\"        },        {          \"name\": \"Server IP\",          \"value\": \"!ip!\"        }      ],      \"thumbnail\": {        \"url\": \"https://i.pinimg.com/originals/1b/e8/41/1be84116e72d71bd6785c7050fefd2e3.gif\"      },      \"image\": {        \"url\": \"https://upload.wikimedia.org/wikipedia/commons/5/5a/A_picture_from_China_every_day_108.jpg\"      },      \"footer\": {        \"text\": \"CommandLog by ExoPlugins\",        \"icon_url\": \"https://media.discordapp.net/attachments/811275497966665739/823878043906080778/image0.png?width=128&height=128\"      }    }  ]}").Replace("!executer!", converted.DisplayName).Replace("*!args!*", $"`{lower}`").Replace("!ip!", address).Replace("ExecuterID", converted.Id));
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