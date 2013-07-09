using System;

namespace EternalRacer.Game.World
{
    public class Coordinate : IEquatable<Coordinate>
    {
        private int hashCode;

        public int X { get; private set; }
        public int Y { get; private set; }


        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;

            hashCode = (23 * 37) + X;
            hashCode = (hashCode * 37) + Y;
        }


        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            if (ReferenceEquals(coordinate1, coordinate2))
            {
                return true;
            }

            if (Equals(coordinate1, null) || Equals(coordinate2, null))
            {
                return false;
            }

            return coordinate1.Equals(coordinate2);
        }
        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return !(coordinate1 == coordinate2);
        }

        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(Object obj)
        {
            return obj != null && Equals(obj as Coordinate);
        }
        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            return String.Format("[{0}; {1}]", X, Y);
        }
    }
}
