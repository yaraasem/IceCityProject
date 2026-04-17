namespace IceCity_W4CC
{
    public class CostStrategyFactory : ICostStrategyFactory
    {
        public ICostCalculationStrategy GetStrategy(string type)
        {
            if (type == "Eco")
                return new EcoCostStrategy();
            else
                return new StandardCostStrategy(); // default
        }
    }

}
