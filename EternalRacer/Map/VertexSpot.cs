using EternalRacer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Map
{
    public class VertexSpot : Spot, IVertex
    {
        public bool IsArticulationPoint { get; private set; }

        public NodeSearching SearchingNode { get; private set; }
        public NodePathing PathingNode { get; private set; }

        public IEnumerable<IVertex> Edges
        {
            get { return RetriveReachableNeighbours.Cast<IVertex>(); }
        }

        public VertexSpot(int x, int y, World worldMap)
            : base(x, y, worldMap)
        {
            SearchingNode = new NodeSearching();
            PathingNode = new NodePathing(this);

            IsArticulationPoint = false;
        }

        public override void SetMyOccupied()
        {
            base.SetMyOccupied();

            foreach (VertexSpot neighbour in CircularNeighbourhood())
            {
                neighbour.DetermineCutVertex();
            }
        }


        public void DetermineCutVertex()
        {
            if (AvailableDirections.Count > 1)
            {
                OccupyMeAs(SpotStates.OccupyIncoming);

                if (AreConnectedOrDoesntMatter(Directions.North, Directions.South) &&
                    AreConnectedOrDoesntMatter(Directions.East, Directions.West) &&
                    AreConnectedOrDoesntMatter(Directions.North, Directions.East) &&
                    AreConnectedOrDoesntMatter(Directions.East, Directions.South) &&
                    AreConnectedOrDoesntMatter(Directions.South, Directions.West) &&
                    AreConnectedOrDoesntMatter(Directions.West, Directions.North))
                {
                    IsArticulationPoint = false;
                }
                else
                {
                    IsArticulationPoint = false;
                }

                SetMeFree();
            }
            else
            {
                IsArticulationPoint = false;
            }
        }

        private bool AreConnectedOrDoesntMatter(Directions firstDirection, Directions secondDirection)
        {
            if (AvailableDirections.Contains(firstDirection) && AvailableDirections.Contains(secondDirection))
            {
                VertexSpot first = (VertexSpot)NeighbourInDirection(firstDirection);
                VertexSpot second = (VertexSpot)NeighbourInDirection(secondDirection);

                return MyWorld.Algorithms.Connected(first, second);
            }

            // Doesn't Matter so true:
            return true;
        }


        public List<VertexSpot> CircularNeighbourhood(int range = 1)
        {
            if (range < 1)
            {
                throw new ArgumentOutOfRangeException("range", range, "Can NOT be smaller then 1.");
            }

            int count = (range * 2) + 1;
            count = (count * count) - 1;

            List<VertexSpot> neighbourhood = new List<VertexSpot>();

            for (int i = (-1) * range; i <= range; ++i)
            {
                for (int j = (-1) * range; j <= range; ++j)
                {
                    if (i != 0 && j != 0)
                    {
                        Coordinate coord = new Coordinate(Coord.X + i, Coord.Y + j);
                        if (MyWorld.IsInsideWorld(coord))
                        {
                            VertexSpot spot = (VertexSpot)MyWorld[coord];
                            if (spot.State == SpotStates.Free)
                            {
                                neighbourhood.Add(spot);
                            }
                        }
                    }
                }
            }

            return neighbourhood;
        }

        public double DistanceTo(IVertex toThat)
        {
            return base.StepsTo((Spot)toThat);
        }

        public Directions DirectionTo(IVertex toThat)
        {
            return base.DirectionToNeighbour((Spot)toThat);
        }
    }
}
