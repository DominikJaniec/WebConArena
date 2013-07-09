using EternalRacer.Graph.Algorithm;
using EternalRacer.Graph.BaseImp;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Game.World
{
    public class Universe : AAlgorithmicGraph<Coordinate>
    {
        private Spot[][] Spots;
        public Spot this[Coordinate coordinate]
        {
            get { return Spots[coordinate.X - Properties.MinCoord.X][coordinate.Y - Properties.MinCoord.Y]; }
            set { Spots[coordinate.X - Properties.MinCoord.X][coordinate.Y - Properties.MinCoord.Y] = value; }
        }
        public Spot this[int X, int Y]
        {
            get { return Spots[X - Properties.MinCoord.X][Y - Properties.MinCoord.Y]; }
            set { Spots[X - Properties.MinCoord.X][Y - Properties.MinCoord.Y] = value; }
        }

        public UniverseProperties Properties { get; private set; }
        public void InitializeUniverse()
        {
            for (int x = 0; x < Properties.HorizontalSpan; ++x)
            {
                for (int y = 0; y < Properties.VerticalSpan; ++y)
                {
                    Spots[x][y] = new Spot(this, new Coordinate(Properties.MinCoord.X + x, Properties.MinCoord.Y + y));
                }
            }

            for (int x = 0; x < Properties.HorizontalSpan; ++x)
            {
                for (int y = 0; y < Properties.VerticalSpan; ++y)
                {
                    Spots[x][y].BindWithNeighbours();
                }
            }
        }

        public Universe(UniverseProperties properties)
        {
            Properties = properties;

            Spots = new Spot[Properties.HorizontalSpan][];
            for (int x = 0; x < Properties.HorizontalSpan; ++x)
            {
                Spots[x] = new Spot[Properties.VerticalSpan];
            }
        }


        public override IEnumerable<Vertex<Coordinate>> Vertices
        {
            get
            {
                for (int x = 0; x < Properties.HorizontalSpan; ++x)
                {
                    for (int y = 0; y < Properties.VerticalSpan; ++y)
                    {
                        Spot spot = Spots[x][y];
                        if (spot.AvailableVertices.Count() >= 1)
                        {
                            yield return spot;
                        }
                    }
                }

                yield break;
            }
        }

        public override bool IsValidId(Coordinate vertexId)
        {
            if (vertexId != null)
            {
                if (vertexId.X >= Properties.MinCoord.X && vertexId.X <= Properties.MaxCoord.X &&
                    vertexId.Y >= Properties.MinCoord.Y && vertexId.Y <= Properties.MaxCoord.Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}