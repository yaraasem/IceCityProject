using System;

namespace IceCity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the owner's name: ");
            string owner_name=Console.ReadLine();

            
            double[] heatervals = new double[30];
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("Enter the heater value of day " +(i+1) + ":" );
                heatervals[i]=double.Parse(Console.ReadLine());
                if (heatervals[i] <= 0)
                {
                    Console.WriteLine("invalid input! please enter positive value");
                    i--;
                }


            }


            double[] workinghours = new double[30];
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("Enter the working hours for day " + (i + 1) + ":");
                workinghours[i] = double.Parse(Console.ReadLine());
                if (workinghours[i] < 0 || workinghours[i] > 24)
                {
                    Console.WriteLine("invalid input! please enter value from 0 to 24");
                    i--;
                }
            }

            double totalworkinghours=TotalWorkingHours(workinghours);
            double median= CalcMedian(heatervals);
            double avaragecost = MonthlyAvrageCost(median, totalworkinghours);
            Console.WriteLine("the owner's name is " + owner_name);
            Console.WriteLine("The total working hours for the heater this month is " + totalworkinghours);
            Console.WriteLine("The avarage cost is " + avaragecost);
            



        }

        static double TotalWorkingHours(double[] workighours)
        { 
            double total = 0;

            for (int i = 0; i < workighours.Length; i++)
            {
                total += workighours[i];
            }
            return total;
        
        }

        static double CalcMedian(double[] heatervals)
        {
            int size = heatervals.Length;
            bool sorted=false;
            while(!sorted)
            {
                sorted = true;
                for (int i = 0; i < size - 1; i++)
                {
                    if (heatervals[i] > heatervals[i + 1])
                    {
                    double temp = heatervals[i];
                    heatervals[i]= heatervals[i+1];
                    heatervals[i+1] = temp;
                    sorted=false;
                    }
                }
                size -= 1;
            }
            int median=heatervals.Length/2;
            return heatervals[median];
        }


        static double MonthlyAvrageCost(double median,double workingtime)
        {
            double averagecost = median * (workingtime / (24 * 30));
            return averagecost;

        }


    }

}
