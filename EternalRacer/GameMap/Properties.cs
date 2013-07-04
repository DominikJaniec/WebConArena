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

            Width = XMax - XMin;
            Height = YMax - YMin;
        }

        #endregion
    }
}
