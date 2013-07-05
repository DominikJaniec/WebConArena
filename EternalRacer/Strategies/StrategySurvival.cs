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

        public Directions LastDirection { get; private set; }

        public StrategySurvival(AStrategy lastStrategy)
            : base(lastStrategy) { }

        protected override Directions ComputeNextMovment()
        {
            Dictionary<Spot, int> nextSpotsNumberOfReachableNeighbours = new Dictionary<Spot, int>(3);
            int minimumReachableNeighbours = Int32.MaxValue;

            foreach (Spot spot in Player.RetriveReachableNeighbours)
            {
                int reachableNeighbours = spot.PossibleDirections.Count();
                //--reachableNeighbours; // Becouse, We can not go back...

                nextSpotsNumberOfReachableNeighbours.Add(spot, reachableNeighbours);

                if (reachableNeighbours < minimumReachableNeighbours)
                {
                    minimumReachableNeighbours = reachableNeighbours;
                }
            }

            if (minimumReachableNeighbours < Int32.MaxValue)
            {
                IEnumerable<Spot> nextSpots = nextSpotsNumberOfReachableNeighbours
                    .Where(d => d.Value == minimumReachableNeighbours).Select(d => d.Key);

                Directions movmentDirection = Player.DirectionToNeighbour(nextSpots.RandomOne());

                if(!Player.PossibleDirections.Contains(movmentDirection)){
                    throw new InvalidOperationException("Gracz nie może się przsunąć?");
                }

                LastDirection = movmentDirection;
            }

            return LastDirection;
        }
    }
}
