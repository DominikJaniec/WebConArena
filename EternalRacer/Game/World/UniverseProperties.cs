using System;

namespace EternalRacer.Game.World
{
    public class UniverseProperties
    {
        public Coordinate MinCoord { get; private set; }
        public Coordinate MaxCoord { get; private set; }

        public int HorizontalSpan { get; private set; }
        public int VerticalSpan { get; private set; }

        public UniverseProperties(int minX, int maxX, int minY, int maxY)
        {
            MinCoord = new Coordinate(minX, minY);
            MaxCoord = new Coordinate(maxX, maxY);

            HorizontalSpan = maxX - minX + 1;
            VerticalSpan = maxY - minY + 1;
        }

        public override string ToString()
        {
            return String.Format("X <{0}, {1}>: {2} | Y <{3}, {4}>: {5}", MinCoord.X, MaxCoord.X, HorizontalSpan, MinCoord.Y, MaxCoord.Y, VerticalSpan);
        }
    }
}
