using System;

namespace EternalRacer.GameMap
{
    public class Properties
    {
        #region Properties

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int XMin { get; private set; }
        public int XMax { get; private set; }
        public int YMin { get; private set; }
        public int YMax { get; private set; }

        #endregion

        #region Constructor

        public Properties(int xMin, int xMax, int yMin, int yMax)
        {
            XMin = xMin;
            XMax = xMax;

            YMin = yMin;
            YMax = yMax;

            Width = XMax - XMin + 1;
            Height = YMax - YMin + 1;
        }

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("X <{0}, {1}>: {2} | Y <{3}, {4}>: {5}", XMin, XMax, Width, YMin, YMax, Height);
        }
        #endregion
    }
}
