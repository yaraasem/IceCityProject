using System;

namespace Ice_City_W3
{
    public class ElectricHeater : Heater
    {
        public ElectricHeater(double heaterPower) : base(heaterPower) { }
        public override double CalcEffectivePower() => HeaterPower;
    }

    public class GasHeater : Heater
    {
        public GasHeater(double heaterPower) : base(heaterPower) { }
        public override double CalcEffectivePower() => HeaterPower * 0.75;
    }
}
