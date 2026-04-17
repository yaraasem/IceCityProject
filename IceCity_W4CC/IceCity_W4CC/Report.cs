namespace IceCity_W4CC
{
    public class Report
    {
        private CostService costService;

        public Report(CostService costService)
        {
            this.costService = costService;
        }

        public string GenerateHouseReport(House house)
        {
            double totalHours = costService.GetTotalHours(house.GetDailyUsages());
            double median = costService.GetMedian(house.Heaters);
            double cost = costService.GetCost(house.GetDailyUsages(), house.Heaters);

            return "Total working hours : " + totalHours.ToString("F2") + "\n" +
                   "Median heater power : " + median.ToString("F2") + " kW\n" +
                   "Monthly cost        : " + cost.ToString("F4");
        }
    }
}
