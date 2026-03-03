using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{
    internal class Report
    {
        private  Service1 service;

        public Report(Service1 service)
        {
            this.service = service;
        }

        public string GenerateHouseReport(House house)
        {
            double totalHours = service.CalcTotalWorkingHours(house.GetDailyUsages());
            double median = service.CalcMedianHeaterValue(house.Heaters);
            double averageCost = service.CalcMonthlyAverageCost(median, totalHours);

            return "Total working hours : " + totalHours.ToString("F2") + "\n" +
                   "Median heater power : " + median.ToString("F2") + " kW\n" +
                   "Monthly average cost: " + averageCost.ToString("F4");
        }
    }
}
