namespace IceCity_W4CC
{
    public class ElectricHeater : Heater
    {
        public ElectricHeater(double heaterPower) : base(heaterPower) { }
        public override double CalcEffectivePower() => HeaterPower;
    }

}

