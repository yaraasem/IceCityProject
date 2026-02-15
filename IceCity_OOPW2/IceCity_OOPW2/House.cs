using System;


namespace IceCity_OOPW2
{
    internal class House
    {
        //the members of these arrays will be objects
        private Heater[] heaters;//num of heaters
        private DailyUsage[] dailyUsage; //num of days =30

        private int heatercount;
        private int dayscount;

        public House(int heaters_num,int numOfDays)
        {
            heaters=new Heater[heaters_num];  //size of array will be the num of heaters in the house 
            dailyUsage=new DailyUsage[numOfDays];
            
            heatercount=0;
            dayscount=0;
        }

        public bool AddHeaters(Heater heater)
        { 
            if (heatercount<heaters.Length)
            {
                heaters[heatercount] = heater;
                heatercount++;
                return true;
            }
           return false;
                
        }


        public bool AddDailyUsage (DailyUsage day)
        {
            if (dayscount < dailyUsage.Length)
            {
                dailyUsage[dayscount] = day;
                dayscount++;
                return true;
            }
            return false;
        }

        public Heater[] GetHeaters() { return heaters; }
        public int HeaterCount { get { return heatercount; } }

        public DailyUsage[] GetDailyUsages() { return dailyUsage; }
        public int DaysCount { get { return dayscount; } }

    }
}
