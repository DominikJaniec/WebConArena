using EternalRacer.GameMap;
using System;

namespace EternalRacer.GameStrategies
{
    public class StrategyRivalry : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Rivalry; }
        }

        public StrategyRivalry(World map, Spot player, Spot enemy)
            : base(map, player, enemy) { }

        protected override Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
