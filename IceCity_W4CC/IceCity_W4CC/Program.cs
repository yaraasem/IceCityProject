
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace IceCity_W4CC
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter owner's name: ");
            string ownerName = Console.ReadLine();
            if (string.IsNullOrEmpty(ownerName)) ownerName = "Unknown";

            Owner owner = new Owner(ownerName);
            House house = new House();
            owner.AddHouse(house);

            SaveDailyUsageDelegate saveWithFullDetails = delegate (DailyUsage usage)
            {
                Console.WriteLine("  [Saver-Full] Saved: " + usage.ToString());
            };
            house.OnSaveDailyUsage += saveWithFullDetails;

            Console.Write("\nEnter number of heaters: ");
            int numHeaters = int.Parse(Console.ReadLine());

            for (int i = 0; i < numHeaters; i++)
            {
                Console.Write("Heater " + (i + 1) + " — Electric, Gas or Solar? (E/G/S): ");
                string type = Console.ReadLine().ToUpper();

                Console.Write("Heater " + (i + 1) + " power (kW): ");
                double power = double.Parse(Console.ReadLine());

                Heater heater;
                if (type == "G")
                    heater = new GasHeater(power);
                else if (type == "S")
                    heater = new SolarHeater(power); 
                else
                    heater = new ElectricHeater(power);

                heater.HeaterOpened += delegate (object s, HeaterOpenEventArgs e)
                {
                    Console.WriteLine("  [Event] OpenHeater fired at " + e.StartTime.ToString("HH:mm:ss"));
                };

                house.AddHeater(heater);
            }

            
            Console.WriteLine("\n--- Simulating Heater Open/Close ---");

            for (int j = 0; j < house.Heaters.Count; j++)
            {
                Heater heater = house.Heaters[j];

                if (!heater.IsActive)
                {
                    Console.WriteLine("[House] Heater#" + heater.heaterID + " is inactive.");
                    continue;
                }

                try
                {
                    if (j == 0 && numHeaters > 1)
                        throw new HeaterFailedException(heater.heaterID,
                            "Simulated failure on Heater#" + heater.heaterID);

                    heater.Open();
                    await Task.Delay(100);
                    heater.Close();
                }
                catch (HeaterFailedException ex)
                {
                    Console.WriteLine("\n[ERROR] " + ex.Message);

                    Heater replacement = await CityCenterService.RequestReplacementAsync(
                        house.HouseId, ex.HeaterID);

                    if (replacement != null)
                    {
                        replacement.HeaterOpened += delegate (object s, HeaterOpenEventArgs e)
                        {
                            Console.WriteLine("  [Event] Replacement OpenHeater at " + e.StartTime.ToString("HH:mm:ss"));
                        };

                        house.ReplaceHeater(ex.HeaterID, replacement);
                        replacement.Open();
                        await Task.Delay(100);
                        replacement.Close();
                    }
                    else
                    {
                        house.DeactivateHeater(ex.HeaterID);
                    }
                }
            }

            
            Console.WriteLine("\n--- Fetching last month weather data ---");
            WeatherService weatherService = new WeatherService();
            try
            {
                List<DailyUsage> weatherUsages =
                    await weatherService.FetchLastMonthWeatherAsync();

                foreach (DailyUsage wu in weatherUsages)
                    house.AddDailyUsage(wu);

                Console.WriteLine("Added " + weatherUsages.Count + " weather-based records.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[WeatherService] Failed: " + ex.Message);
                SimulateLastMonthData(house);
            }

            //  Threads & Tasks
            PrintService.PrintLastMonthDailyUsageWithThreads(house);
            await PrintService.PrintLastMonthDailyUsageWithTasksAsync(house);

            //  Strategy Pattern 
            Console.WriteLine("\n===== HOUSE REPORT =====");
            Console.WriteLine("Owner: " + ownerName);

            // Factory بتقرر الـ Strategy 
            ICostStrategyFactory factory = new CostStrategyFactory();

            Console.Write("\nCost strategy (Standard/Eco): ");
            string strategyType = Console.ReadLine();

            // CostService مش عارفة مين الـ Strategy بالظبط 
            ICostCalculationStrategy strategy = factory.GetStrategy(strategyType);
            CostService costService = new CostService(strategy);
            Report report = new Report(costService);

            Console.WriteLine(report.GenerateHouseReport(house));

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void SimulateLastMonthData(House house)
        {
            Console.WriteLine("[Simulation] Adding fake last-month data...");
            DateTime now = DateTime.UtcNow;
            DateTime start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            Random random = new Random(42);

            for (int d = 0; d < 10; d++)
            {
                double hours = random.Next(4, 14);
                double val = random.Next(500, 2500);
                house.AddDailyUsage(new DailyUsage(start.AddDays(d), hours, val));
            }
        }
    }

}

