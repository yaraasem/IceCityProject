using System;

namespace IceCity_OOPW2
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            Console.Write("Enter owner's name: ");
            string ownerName = Console.ReadLine();

            Console.WriteLine("How many houses do you have?");
            int houses=int.Parse(Console.ReadLine());

            Owner owner = new Owner(ownerName, houses);

            
            Console.Write("Enter number of heaters: ");
            int numHeaters = int.Parse(Console.ReadLine());

            
            Console.Write("Enter number of days for this month: "); 
            int numDays = int.Parse(Console.ReadLine());

            
            House house = new House(numHeaters, numDays);
            owner.AddHouse(house);

            
            for (int i = 0; i < numHeaters; i++)
            {
                Console.Write($"Is heater {i + 1} Electric or Gas? (E/G): ");
                string type = Console.ReadLine().ToUpper();

                Console.Write($"Enter heater {i + 1} power value: ");
                double power = double.Parse(Console.ReadLine());

                if (type == "E")
                    house.AddHeaters(new ElectricHeater(power));
                else
                    house.AddHeaters(new GasHeater(power));
            }

            for (int i = 0; i < numDays; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine($"\n--- Day {i + 1} ---");

                        Console.Write("Enter working hours (0-24): ");
                        double hours = double.Parse(Console.ReadLine());

                        Console.Write("Enter heater value (positive): ");
                        double heaterValue = double.Parse(Console.ReadLine());

                        DailyUsage day = new DailyUsage(DateTime.Now.AddDays(i), hours, heaterValue);//check the constrains
                        house.AddDailyUsage(day);

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);//Exception handeler
                        Console.WriteLine("Try again...");
                    }
                }
            }

            
            // Calculations using Service1 and Report
            Service1 service = new Service1();
            Report report = new Report(service);

            string result = report.GenerateHouseReport(house);

            // Output (same as Week 1 but formatted)
            Console.WriteLine("\n===== HOUSE REPORT =====");
            Console.WriteLine("Owner Name: " + ownerName);
            Console.WriteLine(result);
           

        }
    }
}
//sorry i couldn't implement the idea of repository class i will check it later😊


