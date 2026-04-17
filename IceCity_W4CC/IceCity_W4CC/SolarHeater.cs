namespace IceCity_W4CC
{
    public class SolarHeater : Heater
    {
        public SolarHeater(double heaterPower) : base(heaterPower) { }

        public override double CalcEffectivePower()
        {
            return HeaterPower * 0.70;
        }
    }
}
