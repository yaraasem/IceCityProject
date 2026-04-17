using System;
using System.Collections.Generic;

namespace IceCity_W4CC
{
    public class EcoCostStrategy : ICostCalculationStrategy
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
                values[i] = active[i].CalcEffectivePower();//بحسب الي شغالين بس


            Array.Sort(values);

            int middle = count / 2;

            if (count.IsEven())
                return (values[middle - 1] + values[middle]) / 2.0;
            else
                return values[middle];
        }

        
        public double CalculateCost(double median, double totalHours)
        {
            double cost = median * (totalHours / 720.0);

            if (totalHours < 120)
            {
                cost = cost * 0.90; // خصم 10%
                Console.WriteLine("  [EcoStrategy] Discount applied — Total hours < 120");
            }

            return cost;
        }
    }
}
