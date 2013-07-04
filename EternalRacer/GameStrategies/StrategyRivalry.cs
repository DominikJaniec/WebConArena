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

        #region Override ToString
        public override string ToString()
        {
            return String.Format("Strategy: {0} - {1}", Strategies.Rivalry, base.ToString());
        }
        #endregion
    }
}
