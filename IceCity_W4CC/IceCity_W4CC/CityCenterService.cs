using System;
using System.Threading.Tasks;

namespace IceCity_W4CC
{
    public static class CityCenterService
    {
        public static async Task<Heater> RequestReplacementAsync(int houseId, int heaterId)
        {
            Console.WriteLine($"\n[CityCenterService] Requesting replacement for Heater#{heaterId} in House#{houseId}...");
            await Task.Delay(500);

            var replacement = new ElectricHeater(1500);
            Console.WriteLine($"[CityCenterService] Replacement Heater#{replacement.heaterID} dispatched.\n");
            return replacement;
        }
    }

}

