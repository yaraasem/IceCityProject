using System;


namespace Ice_City_W3
{



    public abstract class Heater
    {
        private double heaterPower;
        public int heaterID;
        private DateTime? lastOpenTime; //nullable
        public static int counter = 0; //related to the whole class 
        public bool IsActive { get; set; }


        public double HeaterPower
        {
            get => heaterPower;
            set
            {
                if (value < 0)
                    throw new ArgumentException("HeaterPower must be >= 0");
                heaterPower = value;
            }

        }

        protected Heater(double heaterPower)
        {
            this.heaterPower = heaterPower;
            heaterID = counter++; //eachtime we create a heater the id will be inc.
            IsActive = true;

        }

        public event HeaterEventHandler HeaterOpened;
        public event HeaterDurationHandler HeaterClosed;


        public void Open()
        {

            if (!IsActive)
            {
                Console.WriteLine("[Heater " + heaterID + "] Is inactive — Open() ignored.");
                return;
            }
            lastOpenTime = DateTime.UtcNow;
            var open_args = new HeaterOpenEventArgs(lastOpenTime.Value);
            HeaterOpened?.Invoke(this, open_args);
            Console.WriteLine($"[Heater {heaterID}] Opened at {lastOpenTime:HH:mm:ss}");
        }

       
        public void Close()
        {
            if (lastOpenTime == null)
            {
                Console.WriteLine($"[Heater {heaterID}] Was not open — Close() ignored.");
                return;
            }

            var end = DateTime.UtcNow;
            var args = new HeaterDurationEventArgs(lastOpenTime.Value, end, CalcEffectivePower());
            lastOpenTime = null; //###

            HeaterClosed?.Invoke(this, args);
            Console.WriteLine($"[Heater {heaterID}] Closed — Hours worked: {args.HoursWorked:F4}");
        }

        public abstract double CalcEffectivePower();
    }


}

