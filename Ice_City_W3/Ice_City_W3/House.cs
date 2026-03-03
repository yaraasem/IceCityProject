using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{
    public class House
    {



        private List<Heater> heaters = new List<Heater>();
        private List<DailyUsage> dailyUsages = new List<DailyUsage>();

        public int HouseId { get; }
        public static int idCounter = 0;


        public SaveDailyUsageDelegate OnSaveDailyUsage;
        public List<Heater> Heaters { get { return heaters; } }

        public House()
        {
            HouseId = ++idCounter;
        }


        public void AddHeater(Heater heater)
        {
            if (heater == null) return;
            heaters.Add(heater);
            heater.HeaterClosed += OnHeaterClosed; //is closed by defult
        }


        public void DeactivateHeater(int heaterId)
        {
            Heater heater = heaters.Find(h => h.heaterID == heaterId);//find the heater that matches the sent ID
            /*
            private Heater FindHeaterById(Heater h)
            {
            return h.HeaterId == searchId;
            }
            */

            if (heater != null) // لو قيمته null اذا مش شغال او مش موجود
            {
                heater.IsActive = false;
                Console.WriteLine("[House] Heater#" + heaterId + " deactivated.");
            }
        }

        // استبدال سخان باظ بسخان جديد في نفس الـ slot
        public void ReplaceHeater(int heaterId, Heater newHeater)
        {
            int index = heaters.FindIndex(h => h.heaterID == heaterId);
            if (index >= 0)
            {
                heaters[index].IsActive = false;   // عطّل القديم
                heaters[index] = newHeater;         //حط البديل
                newHeater.HeaterClosed += OnHeaterClosed; //multicast delegate
                Console.WriteLine("[House] Heater#" + heaterId + " replaced with Heater#" + newHeater.heaterID);
            }
        }



        private void OnHeaterClosed(object sender, HeaterDurationEventArgs e) //same segnature with del
        {
            DailyUsage usage = new DailyUsage(DateTime.UtcNow.Date, e.HoursWorked, e.HeaterValue);
            dailyUsages.Add(usage);

            if (OnSaveDailyUsage != null)
                OnSaveDailyUsage.Invoke(usage);//invoke the del
        }


        public void AddDailyUsage(DailyUsage usage)
        {
            dailyUsages.Add(usage);                  // تضيف بس
            if (OnSaveDailyUsage != null)
                OnSaveDailyUsage.Invoke(usage);
        }
        public List<DailyUsage> GetDailyUsages()
        {
            return dailyUsages;
        }

        public List<DailyUsage> GetLastMonthUsages()
        {
            DateTime now = DateTime.UtcNow;
            DateTime firstOfPrev = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
            DateTime lastOfPrev = new DateTime(now.Year, now.Month, 1).AddDays(-1);

            return dailyUsages
                .Where(u => u.Date >= firstOfPrev.Date && u.Date <= lastOfPrev.Date) //constrains on generics
                .ToList();
        }


        public int HeaterCount { get { return heaters.Count(h => h.IsActive); } }//exclude deactivated
        public int DaysCount { get { return dailyUsages.Count; } }
    }
}
