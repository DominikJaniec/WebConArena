using EternalRacer.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Game.Strategy
{
    public abstract class AStrategy
    {
        public abstract Strategies Kind { get; }

        public Universe Map { get; private set; }
        public Spot Player { get; private set; }
        public Spot Enemy { get; private set; }

        public Directions PlayerLastDirection { get; private set; }


        public AStrategy(Universe map)
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


        public Directions NextMove(Coordinate playerNow, Coordinate enemyNow)
        {
            Player = Map[playerNow];
            Player.State = SpotState.Player;

            Enemy = Map[enemyNow];
            Enemy.State = SpotState.Enemy;

            IEnumerable<Directions> legalDirections = Player.MovementDirections;
            if (legalDirections.Count() > 1)
            {
                Directions computedDirection = ComputeNextMovement();

                if (!Player.MovementDirections.Contains(computedDirection))
                {
                    throw new InvalidOperationException("MovementDirections does NOT contains calculated Direction.");
                }

                PlayerLastDirection = computedDirection;
            }
            else if (legalDirections.Any())
            {
                PlayerLastDirection = legalDirections.Single();
            }

            Player.State = SpotState.Disabled;
            Enemy.State = SpotState.Disabled;

            return PlayerLastDirection;
        }
        protected abstract Directions ComputeNextMovement();


        public override string ToString()
        {
            return String.Format("{0} - Player: {1}, Enemy: {2}", Kind, Player.Id, Enemy.Id);
        }
    }
}
