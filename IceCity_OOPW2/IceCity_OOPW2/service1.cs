using System;

namespace IceCity_OOPW2
{
    internal class Service1
    {
        
        public double CalcTotalWorkingHours(DailyUsage[] dailyUsages, int daysCount)
        {
            double total = 0;
            for (int i = 0; i < daysCount; i++)
            {
                total += dailyUsages[i].WorkHours;
            }
            return total;
        }

        
        public double CalcMedianHeaterValue(Heater[] heaters, int heaterCount)
        {
            double[] values = new double[heaterCount];
            for (int i = 0; i < heaterCount; i++)
            {
                values[i] = heaters[i].CalcEffictivePower();
            }

            Array.Sort(values);

            int middle = heaterCount / 2;
            if (heaterCount % 2 == 0)
            {
                return (values[middle - 1] + values[middle]) / 2;
            }
            else
            {
                return values[middle];
            }
        }

      
        public double CalcMonthlyAverageCost(double median, double totalWorkingHours)
        {
            return median * (totalWorkingHours / 720.0);
        }
    }
}

