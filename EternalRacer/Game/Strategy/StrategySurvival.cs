using EternalRacer.Game.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Game.Strategy
{
    public class StrategySurvival : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Survival; }
        }

        public StrategySurvival(AStrategy lastStrategy)
            : base(lastStrategy) { }

        protected override Directions ComputeNextMovement()
        {
            int minimumReachableNeighbours = Int32.MaxValue;
            Dictionary<Spot, int> nextSpotNumberOfReachableNeighbours = new Dictionary<Spot, int>(3);

            foreach (Spot nextSpot in Player.AvailableVertices)
            {
                int nextReachableNeighbours = nextSpot.AvailableVertices.Count();
                nextSpotNumberOfReachableNeighbours.Add(nextSpot, nextReachableNeighbours);

                if (nextReachableNeighbours < minimumReachableNeighbours)
                {
                    minimumReachableNeighbours = nextReachableNeighbours;
                }
            }

            IEnumerable<Spot> nextSpots = nextSpotNumberOfReachableNeighbours
                .Where(d => d.Value == minimumReachableNeighbours).Select(d => d.Key);

            Directions nextDirection = Player.DirectionToNeighbour(nextSpots.RandomOne());
            return nextDirection;
        }
    }
}
