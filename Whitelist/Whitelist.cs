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
using Steamworks;

namespace Whitelist
{
    public class Whitelist : RocketPlugin
    {
        protected override void Load()
        {
            RocketServerEvents.OnPlayerConnected += Events_OnPlayerConnected;
        }

        void Events_OnPlayerConnected(RocketPlayer player)
        {
            WebClient webClient = new WebClient();

            CSteamID cSteamId = player.CSteamID;
            string whitelisted = webClient.DownloadString("http://unturned.de/steam.php?steamId=" + cSteamId.ToString());
            if (whitelisted != "true")
            {
                player.Kick("Bitte registriere dich auf http://unturned.de/, um dem Server joinen zu können");
            }
        }
    }
}
