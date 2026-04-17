using System;
using System.Collections.Generic;

namespace IceCity_W4CC
{
    public class StandardCostStrategy : ICostCalculationStrategy
    {
        public double CalculateTotalHours(List<DailyUsage> usages)
        {
            double total = 0;
            foreach (DailyUsage u in usages)
                total += u.WorkHours;
            return total;
        }

        public double CalculateMedian(List<Heater> heaters)
        {
            
            List<Heater> active = new List<Heater>();
            foreach (Heater h in heaters)
            {
                if (h.IsActive)
                    active.Add(h);
            }

            int count = active.Count;
            if (count == 0) return 0;

            double[] values = new double[count];
            for (int i = 0; i < count; i++)
                values[i] = active[i].CalcEffectivePower();

            Array.Sort(values);

            int middle = count / 2;

            
            if (count.IsEven())
                return (values[middle - 1] + values[middle]) / 2.0;
            else
                return values[middle];
        }

        public double CalculateCost(double median, double totalHours)
        {
            return median * (totalHours / 720.0);
        }
    }
}
