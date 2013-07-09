using EternalRacer.Game.World;
using System;

namespace EternalRacer.Game.Strategy
{
    public class StrategyRivalry : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Rivalry; }
        }

        public StrategyRivalry(Universe map)
            : base(map) { }

        protected override Directions ComputeNextMovement()
        {
            throw new NotImplementedException();
        }
    }
}
