using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{
    internal class Service1
    {

        public double CalcTotalWorkingHours(List<DailyUsage> usages)
        {
            double total = 0;
            foreach (DailyUsage u in usages)
                total += u.WorkHours;
            return total;
        }

        public double CalcMedianHeaterValue(List<Heater> heaters)
        {
            List<Heater> validHeaters = new List<Heater>();
            foreach (Heater h in heaters)
            {
                if (h.IsActive)
                    validHeaters.Add(h);
            }

            int count = validHeaters.Count;
            if (count == 0) return 0;

            
            double[] values = new double[count];
            for (int i = 0; i < count; i++)
            {
                values[i] = validHeaters[i].CalcEffectivePower();
            }

            Array.Sort(values);

            int middle = count / 2;

            
            double median;
            if (count.IsEven())
                median = (values[middle - 1] + values[middle]) / 2.0;
            else
                median = values[middle];
            
            return median;

        }

        public double CalcMonthlyAverageCost(double median, double totalWorkingHours)
        {
            return median * (totalWorkingHours / 720.0);
        }


    }
}
