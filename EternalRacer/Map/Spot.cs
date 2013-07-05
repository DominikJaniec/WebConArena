using EternalRacer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Map
{
    /// <summary>
    /// Represent a spot node in World.
    /// </summary>
    public class Spot : ISearchNodeProvider
    {
        /// <summary>
        /// Spot Coordinate in World.
        /// </summary>
        public Coordinate Coord { get; private set; }

        /// <summary>
        /// Spot state as SpotStates.
        /// </summary>
        public SpotStates State { get; set; }

        /// <summary>
        /// List of nearest neighbourhood as SpotDirection.
        /// </summary>
        public List<SpotDirection> Neighbourhood { get; private set; }
        /// <summary>
        /// Graph node as ISearchNodeProvider for Graph algorithms.
        /// </summary>
        public SearchNode GraphNode { get; private set; }


        /// <summary>
        /// Spot constructor, sets the read-only Coordinates.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public Spot(int x, int y)
        {
            Coord = new Map.Coordinate(x, y);
        }

        /// <summary>
        /// Initialize Spot's World context. Required for work.
        /// </summary>
        /// <param name="worldMap">World maps with Spots</param>
        /// <param name="spotState">Initialize SpotStates</param>
        /// <exception cref="InvalidOperationException"/>
        public void InitializeInWorld(World worldMap, SpotStates spotState = SpotStates.Free)
        {
            Neighbourhood = new List<SpotDirection>(4);
            GraphNode = new SearchNode();
            State = spotState;

            if (!worldMap.IsInsideWorld(Coord))
            {
                throw new InvalidOperationException("Current Spot is outside of the worldMap");
            }

            // Northern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X, Coord.Y - 1, worldMap);

            // Eastern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X + 1, Coord.Y, worldMap);

            // Southern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X, Coord.Y + 1, worldMap);

            // Western neighbour:
            AddNeighbourIfInsideWolrd(Coord.X - 1, Coord.Y, worldMap);
        }

        private void AddNeighbourIfInsideWolrd(int nX, int nY, World worldMap)
        {
            Coordinate neighbourCoord = new Coordinate(nX, nY);

            if (worldMap.IsInsideWorld(neighbourCoord))
            {
                Neighbourhood.Add(new SpotDirection(this, worldMap[neighbourCoord]));
            }
        }


        /// <summary>
        /// Retrive all possible Directions.
        /// </summary>
        public IEnumerable<Directions> RetrivePossibleDirections
        {
            get { return Neighbourhood.Select(sd => sd.Direction); }
        }
        /// <summary>
        /// Retrive all available neighbours.
        /// </summary>
        public IEnumerable<Spot> RetriveAvailableNeighbours
        {
            get { return Neighbourhood.Select(sd => sd.ToThat); }
        }


        /// <summary>
        /// Number of steps in Taxicab geometry.
        /// From this Spot to toThat Spot.
        /// </summary>
        /// <param name="toThat">Goal Spot</param>
        /// <returns>Integer number of Manhattan distance</returns>
        /// <exception cref="ArgumentNullException"/>
        public int StepsTo(Spot toThat)
        {
            if (toThat == null)
            {
                throw new ArgumentNullException("toThat");
            }

            int dx = Coord.X - toThat.Coord.X;
            dx = (dx < 0) ? (-1) * dx : dx;

            int dy = Coord.Y - toThat.Coord.Y;
            dy = (dy < 0) ? (-1) * dy : dy;

            return (dx + dy);
        }

        /// <summary>
        /// Fix linear Directions.
        /// From this Spot to toThat Spot.
        /// </summary>
        /// <param name="toThat">Goal Spot</param>
        /// <returns>Integer number of Manhattan distance</returns>
        /// <exception cref="ArgumentNullException"/>
        public Directions Direction(Spot toThat)
        {
            if (toThat == null)
            {
                throw new ArgumentNullException("toThat");
            }

            int dx = toThat.Coord.X - Coord.X;
            int dy = toThat.Coord.Y - Coord.Y;

            if (dx == 0 && dy < 0)
            {
                return Directions.North;
            }
            else if (dx > 0 && dy == 0)
            {
                return Directions.East;
            }
            else if (dx == 0 && dy > 0)
            {
                return Directions.South;
            }
            else if (dx < 0 && dy == 0)
            {
                return Directions.West;
            }
            else
            {
                throw new ArgumentOutOfRangeException("toThat", toThat, "It is in an unknown direction.");
            }
        }

        /// <summary>
        /// Get available neighbour in Directions.
        /// </summary>
        /// <param name="direction">Directions to neighbour</param>
        /// <returns></returns>
        public Spot InDirection(Directions direction)
        {
            return Neighbourhood.SingleOrDefault(sd => sd.Direction == direction).ToThat;
        }


        /// <summary>
        /// Returns a string that represents the Coordinates, and State the current Spot.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("{0} - {1}", Coord, State);
        }
    }
}