namespace IceCity_W4CC
{
    public interface ICostStrategyFactory
    {
        ICostCalculationStrategy GetStrategy(string type);
    }
}
