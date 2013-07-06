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

        public Directions PlayerLastDirection { get; private set; }

        public abstract Strategies Kind { get; }

        #endregion

        #region Constructors

        public AStrategy(World map)
        {
            Map = map;
        }

        public AStrategy(AStrategy lastStrategy)
        {
            Map = lastStrategy.Map;
            Player = lastStrategy.Player;
            Enemy = lastStrategy.Enemy;

            PlayerLastDirection = lastStrategy.PlayerLastDirection;
        }

        #endregion

        #region Movment computing

        public Directions NextMove(Coordinate playerNow, Coordinate enemyNow)
        {
            Player = Map[playerNow];
            Enemy = Map[enemyNow];

            if (Player.AvailableDirections.Count > 0)
            {
                PlayerLastDirection = ComputeNextMovment();
            }

            Player.SetMyOccupied();
            Enemy.SetMyOccupied();

            return PlayerLastDirection;
        }

        protected abstract Directions ComputeNextMovment();

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("{0} - Player: {1}, Enemy: {2}", Kind, Player.Coord, Enemy.Coord);
        }
        #endregion
    }
}
