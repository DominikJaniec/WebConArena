using EternalRacer.GameMap;
using System;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    public static class Extensions
    {
        #region On: WebCon.Arena.Bots.AddIn.Point => EternalRacer.GameMap.Spot

        public static Spot ToSpot(this Point point)
        {
            return new Spot(point.X, point.Y);
        }

        #endregion

        #region On: EternalRacer.GameMap.Directions => WebCon.Arena.Bots.AddIn.Move

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
