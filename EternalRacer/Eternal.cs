using EternalRacer.GameMap;
using EternalRacer.GameStrategies;
using System;
using System.AddIn;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    [AddInAttribute("Eternal",
        Version = "0.0.2.1",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        #region Public properties

        public World WorldGameMap { get; private set; }

        public AStrategy GameStrategy { get; private set; }
        public Strategies CurrentStrategy { get; private set; }

        #endregion

        #region Constructor

        public Eternal()
        {
            GetMoveFuns = FirstGetMove;
        }

        #endregion

        #region Method GetMove from IRacer

        public Move GetMove(Point myPosition, Point opponentPosition, List<MapPoint> map)
        {
            return GetMoveFuns(myPosition, opponentPosition, map);
        }

        #endregion

        #region GetMove's implementations

        private Func<Point, Point, List<MapPoint>, Move> GetMoveFuns;

        private Move InGameGetMove(Point myCurrent, Point opponentCurrent, List<MapPoint> map_notUsed)
        {
            return GameStrategy.NextMove(myCurrent.ToSpot(), opponentCurrent.ToSpot()).ToMove();
        }

        private Move FirstGetMove(Point myStartPosition, Point opponentStartPosition, List<MapPoint> mapPointList)
        {
            WorldGameMap = PrepareMap(mapPointList);
            GameStrategy = new StrategyRivalry(WorldGameMap, myStartPosition.ToSpot(), opponentStartPosition.ToSpot());

            GetMoveFuns = InGameGetMove;
            return GetMoveFuns(myStartPosition, opponentStartPosition, mapPointList);
        }

        private World PrepareMap(List<MapPoint> mapPointList)
        {
            int minX = Int32.MaxValue;
            int maxX = Int32.MinValue;
            int minY = Int32.MaxValue;
            int maxY = Int32.MinValue;

            mapPointList.ForEach(mp =>
            {
                if (mp.Point.X < minX)
                {
                    minX = mp.Point.X;
                }
                else if (mp.Point.X > maxX)
                {
                    maxX = mp.Point.X;
                }

                if (mp.Point.Y < minY)
                {
                    minY = mp.Point.Y;
                }
                else if (mp.Point.Y > maxY)
                {
                    maxY = mp.Point.Y;
                }
            });

            Properties mapProperties = new Properties(minX, maxX, minY, maxY);
            return new World(mapProperties);
        }

        #endregion
    }
}
