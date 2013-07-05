using EternalRacer.GameMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.GameStrategies
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
            IEnumerable<Spot> nearestWalkableSpots = Map.NearestWalkableNeighbourhood(Player);
            Dictionary<Spot, int> nextSpotNumberOfOccupies = new Dictionary<Spot, int>(3);

            int maximumNotWalkable = Int32.MinValue;

            foreach (Spot spot in nearestWalkableSpots)
            {
                int occupies = spot.NeighbourhoodNearest.Count(s => Map.IsWalkable(s) == false);
                --occupies; // Becouse, We can not go back...

                nextSpotNumberOfOccupies.Add(spot, occupies);

                if (occupies > maximumNotWalkable)
                {
                    maximumNotWalkable = occupies;
                }
            }

            if (maximumNotWalkable > Int32.MinValue)
            {
                IEnumerable<Spot> nextSpot = nextSpotNumberOfOccupies.Where(d => d.Value == maximumNotWalkable).Select(d => d.Key);
                LastDirection = Player.Direction(nextSpot.RandomOne());
            }

            return LastDirection;
        }
    }
}
