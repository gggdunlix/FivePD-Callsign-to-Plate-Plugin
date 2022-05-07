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

                EventHandlers["FivePD::Client::SpawnVehicle"] += new Action<Ped>(DoPlateToCallsign);
            }

            private void DoPlateToCallsign(Ped ped)
            {

                
                Vehicle PedCar = ped.CurrentVehicle;

                PlayerData playerData = Utilities.GetPlayerData();
                string CallSign = playerData.Callsign;
                
                API.SetVehicleNumberPlateText(PedCar.NetworkId, CallSign);
                

            }
        }
    }
}
