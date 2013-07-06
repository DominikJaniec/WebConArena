using EternalRacer.Graph;
using System;
using System.Collections.Generic;

namespace EternalRacer.Map
{
    /// <summary>
    /// Represent a map of the wolrd.
    /// </summary>
    public class World : IGraph<VertexSpot>
    {
        private readonly VertexSpot[][] WorldMap;


        /// <summary>
        /// Physical boundary of the world.
        /// </summary>
        public Properties Properties { get; private set; }

        /// <summary>
        /// Return Spot on given Coordinate.
        /// </summary>
        /// <param name="coord">Spot's Coordinate</param>
        /// <returns>Spot on given coordinate</returns>
        public VertexSpot this[Coordinate coord]
        {
            get { return WorldMap[coord.X - Properties.XMin][coord.Y - Properties.YMin]; }
            set { WorldMap[coord.X - Properties.XMin][coord.Y - Properties.YMin] = value; }
        }


        /// <summary>
        /// World constructor, sets the read-only properties and create world.
        /// Require invoke InitializeWorld(SpotStates)
        /// </summary>
        /// <param name="properties"></param>
        public World(Properties properties)
        {
            Properties = properties;
            Algorithms = new GraphAlgorithms<World, VertexSpot>(this);


            WorldMap = new VertexSpot[Properties.Width][];
            for (int x = 0; x < Properties.Width; ++x)
            {
                WorldMap[x] = new VertexSpot[Properties.Height];
            }
        }

        /// <summary>
        /// Invoke method InitializeInWorld on each Spot in world map.
        /// Required for work.
        /// </summary>
        /// <param name="worldState">SpotState for all Spots in map</param>
        public void InitializeWorld(SpotStates worldState = SpotStates.Free)
        {
            for (int x = 0; x < Properties.Width; ++x)
            {
                for (int y = 0; y < Properties.Height; ++y)
                {
                    WorldMap[x][y] = new VertexSpot(x, y, this);
                }
            }


            for (int x = 0; x < Properties.Width; ++x)
            {
                for (int y = 0; y < Properties.Height; ++y)
                {
                    WorldMap[x][y].InitializeInWorld();
                }
            }

            for (int x = 0; x < Properties.Width; ++x)
            {
                for (int y = 0; y < Properties.Height; ++y)
                {
                    WorldMap[x][y].InitializeStateInWorld(worldState);
                }
            }
        }


        /// <summary>
        /// Determines whether the given coordinates are in the world.
        /// </summary>
        /// <param name="coord">Coordinate</param>
        /// <returns>Return true if given coordinate are in World boundary</returns>
        public bool IsInsideWorld(Coordinate coord)
        {
            if ((coord.X >= Properties.XMin && coord.X <= Properties.XMax) &&
                (coord.Y >= Properties.YMin && coord.Y <= Properties.YMax))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a string that represents the Properties of the World.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("WorldMap - {0}", Properties);
        }

        /// <summary>
        /// Graph Algorithms instance
        /// </summary>
        public GraphAlgorithms<World, VertexSpot> Algorithms { get; private set; }

        /// <summary>
        /// Enumerator for IGraph
        /// </summary>
        /// <returns>VertexEnumerator</returns>
        public IEnumerable<VertexSpot> GetVertices()
        {
            for (int x = 0; x < Properties.Width; ++x)
            {
                for (int y = 0; y < Properties.Height; ++y)
                {
                    yield return WorldMap[x][y];
                }
            }

            yield break;
        }
    }
}
