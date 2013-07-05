using EternalRacer.Map;
using System;

namespace EternalRacer.Strategies
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

        public AStrategy(World map, Coordinate player, Coordinate enemy)
        {
            Map = map;
            Player = map[player];
            Enemy = map[enemy];
        }

        public AStrategy(AStrategy lastStrategy)
        {
            Map = lastStrategy.Map;
            Player = lastStrategy.Player;
            Enemy = lastStrategy.Enemy;
        }

        #endregion

        #region Movment computing

        public Directions NextMove(Coordinate playerNow, Coordinate enemyNow)
        {
            Map[playerNow].State = SpotStates.Occupy;
            Player = Map[playerNow];

            Map[enemyNow].State = SpotStates.Occupy;
            Enemy = Map[enemyNow];

            return ComputeNextMovment();
        }

        protected abstract Directions ComputeNextMovment();

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("{0} - Player: {1} | Enemy: {2}", Kind, Player.Coord, Enemy.Coord);
        }
        #endregion
    }
}
