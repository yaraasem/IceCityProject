using System;


namespace IceCity_OOPW2
{
    internal class GasHeater : Heater
    {
        public GasHeater(double heaterpower) : base(heaterpower)//the base ctor will be called first so we have to P A
        {
           //no need to assign as it will be assigned at the base class
        }
        public override double CalcEffictivePower()
        {
            return HeaterPower *0.75;  //assuming the heat loss in the gasheater

        }

    }
}
