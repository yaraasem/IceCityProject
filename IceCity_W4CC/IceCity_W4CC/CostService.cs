using System.Collections.Generic;

namespace IceCity_W4CC
{
    public class CostService
    {
        private ICostCalculationStrategy strategy;

        
        public CostService(ICostCalculationStrategy strategy)
        {
            this.strategy = strategy;
        }

        public double GetTotalHours(List<DailyUsage> usages)
        {
            return strategy.CalculateTotalHours(usages);
        }

        public double GetMedian(List<Heater> heaters)
        {
            return strategy.CalculateMedian(heaters);
        }

        public double GetCost(List<DailyUsage> usages, List<Heater> heaters)
        {
            double totalHours = strategy.CalculateTotalHours(usages);
            double median = strategy.CalculateMedian(heaters);
            return strategy.CalculateCost(median, totalHours);
        }
    }
}
