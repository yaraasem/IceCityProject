using System;

namespace IceCity_OOPW2
{
    internal class Report
    {
        private Service1 service;

        public Report(Service1 service)
        {
            this.service = service;
        }

        
        public string GenerateHouseReport(House house)
        {
            double totalWorkingHours = service.CalcTotalWorkingHours(house.GetDailyUsages(), house.DaysCount);
            double median = service.CalcMedianHeaterValue(house.GetHeaters(), house.HeaterCount);
            double averageCost = service.CalcMonthlyAverageCost(median, totalWorkingHours);

            string report = $"Total working hours this month: {totalWorkingHours}\n" +
                            $"Median heater value: {median}\n" +
                            $"Monthly average cost: {averageCost}";

            return report;
        }
    }
}
