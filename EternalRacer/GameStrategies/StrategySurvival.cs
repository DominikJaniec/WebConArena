using System;

namespace EternalRacer.GameStrategies
{
    public class StrategySurvival : AStrategy
    {
        public StrategySurvival(AStrategy lastStrategy)
            : base(lastStrategy) { }

        protected override GameMap.Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
