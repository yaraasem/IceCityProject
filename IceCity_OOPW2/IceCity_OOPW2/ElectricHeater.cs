using System;


namespace IceCity_OOPW2
{
    internal class ElectricHeater:Heater
    {
        public ElectricHeater(double heaterpower) : base(heaterpower)//the base ctor will be called first so we have to P A
        {
            //no need to assign as it will be assigned at the base class
        }
        public override double CalcEffictivePower()
        {
            return HeaterPower  ;  

        }

    }
}
