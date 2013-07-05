using EternalRacer.Map;
using System;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    public static class Extensions
    {
        #region On: WebCon.Arena.Bots.AddIn.Point => EternalRacer.Map.Coordinate

        public static Coordinate ToCoordinate(this Point point)
        {
            return new Coordinate(point.X, point.Y);
        }

        #endregion

        #region On: EternalRacer.Map.Directions => WebCon.Arena.Bots.AddIn.Move

        public static Move ToMove(this Directions direction)
        {
            switch (direction)
            {
                case Directions.North:
                    return Move.Up;
                case Directions.East:
                    return Move.Right;
                case Directions.South:
                    return Move.Down;
                case Directions.West:
                    return Move.Left;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, "It is unknown direction.");
            }
        }

        #endregion
    }
}
