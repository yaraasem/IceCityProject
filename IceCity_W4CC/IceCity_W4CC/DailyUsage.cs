using System;

namespace IceCity_W4CC
{
    public class DailyUsage
    {
        private DateTime date;
        private double workHours;
        private double heaterValue;

        public DailyUsage(DateTime date, double workHours, double heaterValue)
        {
            Date = date;
            WorkHours = workHours;
            HeaterValue = heaterValue;
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public double WorkHours
        {
            get => workHours;
            set
            {
                if (value < 0 || value > 24)
                    throw new ArgumentException("Invalid hours! Must be between 0 and 24.");
                workHours = value;
            }
        }

        public double HeaterValue
        {
            get => heaterValue;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Heater value must be positive.");
                heaterValue = value;
            }
        }

        public override string ToString() =>
            $"{Date:yyyy-MM-dd} | HeaterValue={HeaterValue:F1} | Hours={WorkHours:F2}";
    }

}

