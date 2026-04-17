namespace IceCity_W4CC
{
    public class GasHeater : Heater
    {
        public GasHeater(double heaterPower) : base(heaterPower) { }
        public override double CalcEffectivePower() => HeaterPower * 0.75;
    }

}

