using System.Collections.Generic;

namespace IceCity_W4CC
{
    public interface ICostCalculationStrategy
    {
        double CalculateTotalHours(List<DailyUsage> usages);
        double CalculateMedian(List<Heater> heaters);
        double CalculateCost(double median, double totalHours);
    }
}
