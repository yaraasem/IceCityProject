using System;

namespace Ice_City_W3
{
    
    public class HeaterFailedException : Exception
    {
        public int HeaterID { get; }

        public HeaterFailedException(int heaterId, string message = "Heater has failed!")
            : base(message)
        {
            HeaterID = heaterId;
        }
    }
}

