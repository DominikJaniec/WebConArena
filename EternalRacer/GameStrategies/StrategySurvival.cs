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

        #region Override ToString
        public override string ToString()
        {
            return String.Format("Strategy: {0} - {1}", Strategies.Survival, base.ToString());
        }
        #endregion
    }
}
