using EternalRacer.GameMap;
using System;

namespace EternalRacer.GameStrategies
{
    public class StrategySurvival : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Survival; }
        }

        public StrategySurvival(AStrategy lastStrategy)
            : base(lastStrategy) { }

        protected override Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
