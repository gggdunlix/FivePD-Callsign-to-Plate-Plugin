using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;

namespace PlateToCallsign
{
    public class PlateToCallsign
    {
        public class Plugin : FivePD.API.Plugin
        {
            internal Plugin()
            {
                Events.OnDutyStatusChange += OnDutyStatusChange;
                 
            }
            private async Task OnDutyStatusChange(bool onDuty)
            {
                if (onDuty)
                {
                    EventHandlers["FivePD::Client::SpawnVehicle"] += new Action<int, int>(DoPlateToCallsign); //Natixco
                }
                else
                {
                }
            }
            private void DoPlateToCallsign(int playerServerId, int vehicleNetworkId)
            {
                if (vehicleNetworkId > 0)
                {

                    PlayerData data = Utilities.GetPlayerData();
                    int ActualVehicleID = Game.PlayerPed.CurrentVehicle.NetworkId;
                    string Callsign = data.Callsign;
                    
                    //with help from Grandpa Rex

                    var vehicle = Entity.FromNetworkId(vehicleNetworkId);
                    API.SetVehicleNumberPlateText(vehicle.Handle, Callsign);
                }

            }
        }
    }
}
