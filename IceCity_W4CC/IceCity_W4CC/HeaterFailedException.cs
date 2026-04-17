using System;

namespace IceCity_W4CC
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

