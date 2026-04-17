using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IceCity_W4CC
{
    public static class PrintService //يعني مش محتاج تعمل object ،بتنادي على الميثودز علي طول
    {
        public static void PrintLastMonthDailyUsageWithThreads(House house)
        {
            List<DailyUsage> usages = house.GetLastMonthUsages();
            Console.WriteLine("\n========== [THREADS] Last Month Daily Usage ==========");

            if (usages.Count == 0)
            {
                Console.WriteLine("  No records for last month.");
                return;
            }

            Thread t1 = new Thread(() => PrintUsageWithThreadId(usages, "Thread-1"));
            Thread t2 = new Thread(() => PrintUsageWithThreadId(usages, "Thread-2"));

            t1.Start();
            t2.Start();
            t1.Join();   // Main Thread frozen here
            t2.Join();   // Main Thread frozen here
            // join blocks the main thread
            ////In UI apps it freezes the screen


        }

        private static void PrintUsageWithThreadId(List<DailyUsage> usages, string label)
        {
            foreach (DailyUsage u in usages)
            {
                Console.WriteLine("[" + label + "] " +
                    u.Date.ToString("yyyy-MM-dd") + " | HeaterValue=" + u.HeaterValue.ToString("F1") +
                    " | Hours=" + u.WorkHours.ToString("F2") +
                    " | Thread=" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10);
            }
        }



        public static async Task PrintLastMonthDailyUsageWithTasksAsync(House house)
        {
            List<DailyUsage> usages = house.GetLastMonthUsages();
            Console.WriteLine("\n========== [TASKS] Last Month Daily Usage ==========");

            if (usages.Count == 0)
            {
                Console.WriteLine("  No records for last month.");
                return;
            }
            // task will pick a thread from the thread pool u won't creat new thred 
            // .Net manage the thread pool
            Task[] tasks = new[]
            {
                Task.Run(() => PrintUsageWithTaskId(usages, "Task-A")),
                Task.Run(() => PrintUsageWithTaskId(usages, "Task-B"))
            };

            await Task.WhenAll(tasks);// Main Thread free No blocking here
                                      // comes back when tasks finish
                                      //In UI apps the screen stays responsive

        }

        private static void PrintUsageWithTaskId(List<DailyUsage> usages, string label)
        {
            foreach (DailyUsage u in usages)
            {
                Console.WriteLine("[" + label + "] " +
                    u.Date.ToString("yyyy-MM-dd") + " | HeaterValue=" + u.HeaterValue.ToString("F1") +
                    " | Hours=" + u.WorkHours.ToString("F2") +
                    " | Task=" + Task.CurrentId);
            }
        }

    }
}
