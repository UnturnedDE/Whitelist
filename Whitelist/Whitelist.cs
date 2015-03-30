using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.RocketAPI;
using SDG;
using UnityEngine;
using System.Net;
using Rocket.RocketAPI.Events;
using Rocket.Logging;

namespace Whitelist
{
    public class Whitelist : RocketPlugin
    {
        public WebClient webClient;
        public static Dictionary<string, string> Players = new Dictionary<string, string>();

        protected override void Load()
        {
            RocketServerEvents.OnPlayerConnected += Events_OnPlayerConnected;
        }

        void Events_OnPlayerConnected(Player player)
        {
            SteamPlayerID steamId = player.SteamChannel.SteamPlayer.SteamPlayerID;
            string check = webClient.DownloadString("http://unturned.de/steam.php?steamId=" + steamId.CSteamID.ToString());
            if (check != "true")
            {
                Steam.kick(steamId.CSteamID, Translate("not_registered"));
            }
        }

        public override System.Collections.Generic.Dictionary<string, string> DefaultTranslations
        {
            get
            {
                return new System.Collections.Generic.Dictionary<string, string>() {
                    {"not_registered", "Bitte registriere dich auf Unturned.de, um dem Server joinen zu können."},
                };
            }
        }
    }
}
