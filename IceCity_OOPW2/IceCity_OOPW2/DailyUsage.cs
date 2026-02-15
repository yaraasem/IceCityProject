using System;


namespace IceCity_OOPW2
{
    internal class DailyUsage
    {
        //fields
        private DateTime date;
        private double workinghours;
        private double heatervals;

        //constractor
        public DailyUsage(DateTime date,double workinghours,double heatervals) //will assign the vals to the prop
        {
            Date= date;
            WorkHours = workinghours;
            Heatervals = heatervals;
        }

        //properties
        public DateTime Date 
        {
            get { return date; }
            set { date = value; } 
        }

        public double WorkHours
        {
            get { return workinghours; }

            set
            {
                if (value < 0 || value > 24)
                    throw new ArgumentException("invalid hour! \n It must be from 0 to 24"); //Exeption
                else
                    workinghours = value;
            }
        }

        public double Heatervals
        {
            get { return heatervals; }

            set
            {
                if (value <= 0)
                    throw new ArgumentException("invalid heater value! \n It must be postive");
                else 
                    heatervals = value;
            
            }

        }


    }
}
