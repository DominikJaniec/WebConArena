using EternalRacer.Graph;
using EternalRacer.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Strategies
{
    public class StrategySurvival : AStrategy
    {
        public override Strategies Kind
        {
            get { return Strategies.Survival; }
        }

        public StrategySurvival(AStrategy lastStrategy)
            : base(lastStrategy) { }

        protected override Directions ComputeNextMovment()
        {
            int minimumReachableNeighbours = Int32.MaxValue;
            Dictionary<Spot, int> nextSpotNumberOfReachableNeighbours;
            nextSpotNumberOfReachableNeighbours = new Dictionary<Spot, int>(3);

            foreach (Spot nextSpot in Player.RetriveReachableNeighbours)
            {
                int nextReachableNeighbours = nextSpot.AvailableDirections.Count();

                // Becouse, We can not go back...
                --nextReachableNeighbours;

                nextSpotNumberOfReachableNeighbours.Add(nextSpot, nextReachableNeighbours);

                if (nextReachableNeighbours < minimumReachableNeighbours)
                {
                    minimumReachableNeighbours = nextReachableNeighbours;
                }
            }

            IEnumerable<Spot> nextSpots = nextSpotNumberOfReachableNeighbours
                .Where(d => d.Value == minimumReachableNeighbours).Select(d => d.Key);

            Directions nextDirection = Player.DirectionToNeighbour(nextSpots.RandomOne());

            //TODO: Posprzatac kiedy bedzie działać
            if (!Player.AvailableDirections.Contains(nextDirection))
            {
                throw new InvalidOperationException("Gracz nie może się przsunąć?");
            }

            return nextDirection;
        }
    }
}
