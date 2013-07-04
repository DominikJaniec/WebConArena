using EternalRacer.GameMap;
using System;

namespace EternalRacer.GameStrategies
{
    public class StrategyRivalry : AStrategy
    {
        public StrategyRivalry(World map, Spot player, Spot enemy)
            : base(map, player, enemy) { }

        protected override GameMap.Directions ComputeNextMovment()
        {
            throw new NotImplementedException();
        }
    }
}
