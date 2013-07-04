using System;

namespace EternalRacer.GameMap
{
    public class World
    {
        #region Fields

        public readonly Properties Properties;
        private readonly int[] WorldMapState;

        #endregion

        #region Constructor

        public World(Properties properties)
        {
            Properties = properties;

            WorldMapState = new int[Properties.Width * Properties.Height];
            for (int x = 0; x < Properties.Width; ++x)
            {
                for (int y = 0; y < Properties.Height; ++y)
                {
                    WorldMapState[IndexByXY(x, y)] = SpotState.Free.ToInt();
                }
            }
        }

        #endregion

        #region Checking functions

        public bool IsInsideWorld(Spot point)
        {
            return IsInsideWorld(point.X, point.Y);
        }

        public bool IsInsideWorld(int x, int y)
        {
            if ((x >= Properties.XMin && x <= Properties.XMax) &&
                (y >= Properties.YMin && y <= Properties.YMax))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsWalkable(Spot point)
        {
            return IsWalkable(point.X, point.Y);
        }

        public bool IsWalkable(int x, int y)
        {
            if (IsInsideWorld(x, y) &&
                this[x, y].ToSpotState() == SpotState.Free)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Indexer

        private int IndexByXY(int x, int y)
        {
            return x + (Properties.Width * y);
        }

        public int this[int x, int y]
        {
            get { return WorldMapState[IndexByXY(x, y)]; }
            set { WorldMapState[IndexByXY(x, y)] = value; }
        }

        public SpotState this[Spot point]
        {
            get { return this[point.X, point.Y].ToSpotState(); }
            set { this[point.X, point.Y] = value.ToInt(); }
        }

        #endregion

        #region Override ToString
        public override string ToString()
        {
            return String.Format("WorldMap - {0}", Properties);
        }
        #endregion
    }
}
