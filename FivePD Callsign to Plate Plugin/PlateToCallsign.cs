using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using FivePD.API;
using FivePD.API.Utils;
using CitizenFX.Core.Native;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json.Schema;

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
                    EventHandlers["FivePD::Client::SpawnVehicle"] += new Action<int, int>(CheckDepartment); //Natixco
                }
                else
                {
                }
            }
            private void CheckDepartment(int playerServerID, int vehicleNetworkID) //suggested by Lucky
            {
                PlayerData data = Utilities.GetPlayerData();

                int playerdepartment = data.DepartmentID;
                var file = API.LoadResourceFile(API.GetCurrentResourceName(), "/config/vehicles.json");
                JToken config = JToken.Parse(file);
                if (config["plateToCallsignDepartments"] != null)
                {
                    foreach (int Department in config["plateToCallsignDepartments"])
                    {
                        if (playerdepartment == Department)
                        {
                            DoPlateToCallsign(LocalPlayer.ServerId, Game.PlayerPed.CurrentVehicle.NetworkId);
                        }
                    }
                    
                } else
                {
                    DoPlateToCallsign(LocalPlayer.ServerId, Game.PlayerPed.CurrentVehicle.NetworkId);
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
