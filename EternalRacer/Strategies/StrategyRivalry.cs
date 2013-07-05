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

        public StrategyRivalry(World map, Coordinate player, Coordinate enemy)
            : base(map, player, enemy) { }

        protected override Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
