using EternalRacer.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Map
{
    /// <summary>
    /// Represent a spot node in World.
    /// </summary>
    public class Spot
    {
        protected World MyWorld;

        private void AddNeighbourIfInsideWolrd(int nX, int nY)
        {
            Coordinate neighbourCoord = new Coordinate(nX, nY);

            if (MyWorld.IsInsideWorld(neighbourCoord))
            {
                Neighbourhood.Add(MyWorld[neighbourCoord]);
            }
        }


        /// <summary>
        /// Spot Coordinate in World.
        /// </summary>
        public Coordinate Coord { get; private set; }

        /// <summary>
        /// Spot state as SpotStates.
        /// </summary>
        public SpotStates State { get; private set; }

        /// <summary>
        /// List of nearest neighbour Spot.
        /// </summary>
        public List<Spot> Neighbourhood { get; private set; }
        /// <summary>
        /// List of possible movment as Directions
        /// </summary>
        public List<Directions> AvailableDirections { get; private set; }


        /// <summary>
        /// Spot constructor, sets the read-only Coordinates in world.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        /// <param name="worldMap">World maps with Spots</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        public Spot(int x, int y, World worldMap)
        {
            if (worldMap == null)
            {
                throw new ArgumentNullException("worldMap");
            }

            MyWorld = worldMap;
            Coord = new Coordinate(x, y);

            if (!worldMap.IsInsideWorld(Coord))
            {
                throw new InvalidOperationException("Current Spot is outside of the worldMap");
            }

            Neighbourhood = new List<Spot>(4);
            AvailableDirections = new List<Directions>(4);
        }

        /// <summary>
        /// Initialize Spot's World context.
        /// 1'st. required for work.
        /// </summary>
        public void InitializeInWorld()
        {
            Neighbourhood.Clear();
            AvailableDirections.Clear();

            // Northern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X, Coord.Y - 1);

            // Eastern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X + 1, Coord.Y);

            // Southern neighbour:
            AddNeighbourIfInsideWolrd(Coord.X, Coord.Y + 1);

            // Western neighbour:
            AddNeighbourIfInsideWolrd(Coord.X - 1, Coord.Y);
        }

        /// <summary>
        /// Initialize Spot's World State.
        /// 2'nd. required for work.
        /// </summary>
        /// <param name="spotState">Initialize SpotStates</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void InitializeStateInWorld(SpotStates spotState)
        {
            switch (spotState)
            {
                case SpotStates.Free:
                    SetMeFree();
                    break;
                case SpotStates.Occupied:
                    SetMyOccupied();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("spotState", spotState, "Unknown spot state.");
            }
        }



        /// <summary>
        /// Set State to SpotStates.Free, and establishes possible directions, in this and Neighbourhood.
        /// </summary>
        public void SetMeFree()
        {
            AvailableDirections.Clear();

            foreach (Spot neighbour in Neighbourhood)
            {
                if (neighbour.State == SpotStates.Free)
                {
                    neighbour.DirectionAdd(this);
                    DirectionAdd(neighbour);
                }
            }

            State = SpotStates.Free;
        }

        /// <summary>
        /// Set State to SpotStates.Occupied, and closes possible directions, in this and Neighbourhood.
        /// </summary>
        public virtual void SetMyOccupied()
        {
            State = SpotStates.Occupied;

            foreach (Spot neighbour in Neighbourhood)
            {
                if (neighbour.State != SpotStates.Occupied)
                {
                    neighbour.DirectionRemove(this);
                }
            }

            AvailableDirections.Clear();
        }

        /// <summary>
        /// Set State to SpotStates.OccupyIncoming or SpotStates.OccupyOutgoing, and closes matching directions.
        /// </summary>
        /// <param name="spotState">Can by only: OccupyIncoming or OccupyOutgoing</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public virtual void OccupyMeAs(SpotStates spotState)
        {
            switch (spotState)
            {
                case SpotStates.OccupyIncoming:
                    State = SpotStates.OccupyIncoming;
                    foreach (Spot neighbour in Neighbourhood)
                    {
                        if (neighbour.State != SpotStates.Occupied)
                        {
                            neighbour.DirectionRemove(this);
                        }
                    }
                    break;
                case SpotStates.OccupyOutgoing:
                    State = SpotStates.OccupyOutgoing;
                    AvailableDirections.Clear();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("spotState", spotState, "Can by only: SpotStates.OccupyIncoming or SpotStates.OccupyOutgoing");
            }
        }


        /// <summary>
        /// Remove Direction from AvailableDirections.
        /// From this Spot to toThat Spot.
        /// </summary>
        /// <param name="toThat">Goal Spot</param>
        /// <exception cref="ArgumentNullException"/>
        public void DirectionRemove(Spot toThat)
        {
            if (toThat == null)
            {
                throw new ArgumentNullException("toThat");
            }

            Directions direcction = DirectionToNeighbour(toThat);
            AvailableDirections.Remove(direcction);
        }

        /// <summary>
        /// Add Direction to AvailableDirections.
        /// From this Spot to toThat Spot.
        /// </summary>
        /// <param name="toThat">Goal Spot</param>
        /// <exception cref="ArgumentNullException"/>
        public void DirectionAdd(Spot toThat)
        {
            if (toThat == null)
            {
                throw new ArgumentNullException("toThat");
            }

            Directions direcction = DirectionToNeighbour(toThat);
            if (!AvailableDirections.Contains(direcction))
            {
                AvailableDirections.Add(direcction);
            }
        }



        /// <summary>
        /// Retrive all available neighbours.
        /// </summary>
        public IEnumerable<Spot> RetriveReachableNeighbours
        {
            get { return Neighbourhood.Where(neighbour => AvailableDirections.Contains(DirectionToNeighbour(neighbour))); }
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
        /// designated Directions. From this Spot to neighbour.
        /// </summary>
        /// <param name="neighbour">Goal neighbour</param>
        /// <returns>Directions value</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public Directions DirectionToNeighbour(Spot neighbour)
        {
            if (neighbour == null)
            {
                throw new ArgumentNullException("neighbour");
            }

            int dx = neighbour.Coord.X - Coord.X;
            int dy = neighbour.Coord.Y - Coord.Y;

            if (dx == 0 && dy == -1)
            {
                return Directions.North;
            }
            else if (dx == 1 && dy == 0)
            {
                return Directions.East;
            }
            else if (dx == 0 && dy == 1)
            {
                return Directions.South;
            }
            else if (dx == -1 && dy == 0)
            {
                return Directions.West;
            }
            else
            {
                throw new ArgumentOutOfRangeException("neighbour", neighbour, "It is in an unknown direction.");
            }
        }

        /// <summary>
        /// Get Neighbour in specific directions.
        /// </summary>
        /// <param name="direction">Directions from this to neighbour</param>
        /// <returns>Neighbour Spot</returns>
        public Spot NeighbourInDirection(Directions direction)
        {
            return Neighbourhood.Single(s => DirectionToNeighbour(s) == direction);
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