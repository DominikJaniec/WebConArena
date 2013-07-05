using EternalRacer.Map;
using System;

namespace EternalRacer.Strategies
{
    public class StrategyRivalry : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Rivalry; }
        }

        public StrategyRivalry(World map)
            : base(map) { }

        protected override Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
