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
        WebClient webClient = new WebClient();

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
                Steam.kick(steamId.CSteamID, "Bitte registriere dich auf http://unturned.de/, um dem Server joinen zu können");
            }
        }
    }
}
