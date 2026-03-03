using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ice_City_W3
{

    //the info about the events.
    public class HeaterOpenEventArgs : EventArgs  // return start time
    {
        public DateTime StartTime { get; }
        public HeaterOpenEventArgs(DateTime startTime) => StartTime = startTime; //ctor


    }

    public class HeaterDurationEventArgs : EventArgs 
    {
        //read only
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public double HoursWorked { get; }
        public double HeaterValue { get; }

        public HeaterDurationEventArgs(DateTime start, DateTime end, double heaterValue)
        {
            StartTime = start;
            EndTime = end;
            HoursWorked = (end - start).TotalHours;
            HeaterValue = heaterValue;
        }
    }

    public delegate void HeaterEventHandler(object sender, HeaterOpenEventArgs e);
    public delegate void HeaterDurationHandler(object sender, HeaterDurationEventArgs e);
    public delegate void SaveDailyUsageDelegate(DailyUsage usage);
}
