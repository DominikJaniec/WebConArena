using System;

namespace EternalRacer.Map
{
    /// <summary>
    /// Represent a Wolrd's properties.
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// Span in horizontal.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Span in vertical.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Minimum correct value of X coordinate.
        /// </summary>
        public int XMin { get; private set; }
        /// <summary>
        /// Maximum correct value of X coordinate.
        /// </summary>
        public int XMax { get; private set; }
        /// <summary>
        /// Minimum correct value of Y coordinate.
        /// </summary>
        public int YMin { get; private set; }
        /// <summary>
        /// Maximum correct value of Y coordinate.
        /// </summary>
        public int YMax { get; private set; }


        /// <summary>
        /// World's Properties constructor, sets all read-only properties.
        /// </summary>
        /// <param name="xMin">Lowest value of X coordinate</param>
        /// <param name="xMax">Highest value of X coordinate</param>
        /// <param name="yMin">Lowest value of Y coordinate</param>
        /// <param name="yMax">Highest value of Y coordinate</param>
        public Properties(int xMin, int xMax, int yMin, int yMax)
        {
            XMin = xMin;
            XMax = xMax;

            YMin = yMin;
            YMax = yMax;

            Width = XMax - XMin + 1;
            Height = YMax - YMin + 1;
        }


        /// <summary>
        /// Returns a string that represents the Min & Max X, Y coordinates, and span in World.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return String.Format("X <{0}, {1}>: {2} | Y <{3}, {4}>: {5}", XMin, XMax, Width, YMin, YMax, Height);
        }
    }
}
