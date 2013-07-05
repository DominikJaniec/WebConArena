using System;
namespace EternalRacer.Map
{
    /// <summary>
    /// Represent a coordinate in 2D.
    /// </summary>
    public struct Coordinate
    {
        /// <summary>
        /// X Coordinate.
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Y Coordinate.
        /// </summary>
        public readonly int Y;


        /// <summary>
        /// Coordinate's constructor, sets the read-only X and Y coordinates.
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Returns a string that represents the X & Y coordinates.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("[{0}; {1}]", X, Y);
        }
    }
}
