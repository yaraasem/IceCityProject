using System;


namespace IceCity_OOPW2
{
    internal abstract class Heater
    {
        private double heaterpower;

        public Heater(double heaterpower)
        {
            HeaterPower= heaterpower;
        }

        public double HeaterPower
        { 
            get 
            { 
                return heaterpower; 
            } 
            set  
            {
                if (value < 0)
                    throw new ArgumentException("Must be greater than Zero ");
                else
                    heaterpower = value;  
            } 
        }

        public abstract double CalcEffictivePower();

    }
}
