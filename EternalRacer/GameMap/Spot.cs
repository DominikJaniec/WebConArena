using System;
using System.Collections.Generic;

namespace EternalRacer.GameMap
{
    public struct Spot
    {
        #region Public readonly fields

        public readonly int X;
        public readonly int Y;

        #endregion

        #region Constructor

        public Spot(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        /// <summary>
        /// Number of steps in Taxicab geometry.
        /// </summary>
        /// <param name="toThat">Goal Spot</param>
        /// <returns>Integer of Manhattan distance</returns>
        public int StepsTo(Spot toThat)
        {
            int dx = X - toThat.X;
            dx = (dx < 0) ? (-1) * dx : dx;

            int dy = Y - toThat.Y;
            dy = (dy < 0) ? (-1) * dy : dy;

            return (dx + dy);
        }

        #region Neighbourhood

        public List<Spot> NeighbourhoodNearest
        {
            get
            {
                return NeighbourhoodByRange(1);
            }
        }

        public List<Spot> NeighbourhoodByRange(int range = 1)
        {
            if (range < 1)
            {
                throw new ArgumentOutOfRangeException("range", range, "Can NOT be smaller then 1.");
            }

            int count = (range * 2) + 1;
            count = (count * count) - 1;

            List<Spot> neighbourhooh = new List<Spot>(count);

            for (int i = (-1) * range; i <= range; ++i)
            {
                for (int j = (-1) * range; j <= range; ++j)
                {
                    if (i != 0 || j != 0)
                    {
                        neighbourhooh.Add(new Spot(this.X + i, this.Y + j));
                    }
                }
            }

            return neighbourhooh;
        }

        #endregion

        #region Directions

        public Directions Direction(Spot toThat)
        {
            int dx = toThat.X - this.X;
            int dy = toThat.Y - this.Y;

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
                throw new ArgumentOutOfRangeException("toThat", toThat, "It is in an unknown direction.");
            }
        }

        public Spot InDirection(Directions direction, int steps = 1)
        {
            if (steps < 1)
            {
                throw new ArgumentOutOfRangeException("steps", steps, "Can NOT be smaller then 1.");
            }

            switch (direction)
            {
                case Directions.North:
                    return new Spot(this.X, this.Y - 1);
                case Directions.East:
                    return new Spot(this.X + 1, this.Y);
                case Directions.South:
                    return new Spot(this.X, this.Y + 1);
                case Directions.West:
                    return new Spot(this.X - 1, this.Y);
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, "It is unknown direction.");
            }
        }

        #endregion

        #region Overrides for HashSet

        public override int GetHashCode()
        {
            return this.X << 16 | this.Y;
        }

        public override bool Equals(object obj)
        {
            if (this.GetHashCode() == obj.GetHashCode())
            {
                return obj is Spot;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("[{0}; {1}]", X, Y);
        }
        #endregion
    }
}