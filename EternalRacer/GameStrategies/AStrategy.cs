using EternalRacer.GameMap;
using System;

namespace EternalRacer.GameStrategies
{
    public abstract class AStrategy
    {
        #region Public properties

        public World Map { get; private set; }
        public Spot Player { get; private set; }
        public Spot Enemy { get; private set; }

        public abstract Strategies Kind { get; }

        #endregion

        #region Constructors

        public AStrategy(World map, Spot player, Spot enemy)
        {
            Map = map;
            Player = player;
            Enemy = enemy;
        }

        public AStrategy(AStrategy lastStrategy)
        {
            Map = lastStrategy.Map;
            Player = lastStrategy.Player;
            Enemy = lastStrategy.Enemy;
        }

        #endregion

        #region Movment computing

        public Directions NextMove(Spot playerNow, Spot enemyNow)
        {
            Player = playerNow;
            Map[Player] = SpotState.Occupy;

            Enemy = enemyNow;
            Map[enemyNow] = SpotState.Occupy;

            return ComputeNextMovment();
        }

        protected abstract Directions ComputeNextMovment();

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("{0} - Player: {1} | Enemy: {2}", Kind, Player, Enemy);
        }
        #endregion
    }
}
