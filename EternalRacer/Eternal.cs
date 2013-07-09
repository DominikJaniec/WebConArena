using EternalRacer.Game.Strategy;
using EternalRacer.Game.World;
using System;
using System.AddIn;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    [AddInAttribute("Eternal",
        Version = "0.0.3.13",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        #region Public properties

        public Universe WorldGameMap { get; private set; }
        public AStrategy GameStrategy { get; private set; }

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
            return GameStrategy.NextMove(myCurrent.ToCoordinate(), opponentCurrent.ToCoordinate()).ToMove();
        }

        private Move FirstGetMove(Point myStartPosition, Point opponentStartPosition, List<MapPoint> mapPointList)
        {
            PrepareGameMap(mapPointList);
            GameStrategy = new StrategyRivalry(WorldGameMap);

            //TODO 1: Implementacja strategi przetrwania.
            // Z tego powodu tylko strategi przetrwania:
            GameStrategy = new StrategySurvival(GameStrategy);

            GetMoveFuns = InGameGetMove;
            return GetMoveFuns(myStartPosition, opponentStartPosition, mapPointList);
        }

        private void PrepareGameMap(List<MapPoint> mapPointList)
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

            UniverseProperties mapProperties = new UniverseProperties(minX, maxX, minY, maxY);

            WorldGameMap = new Universe(mapProperties);
            WorldGameMap.InitializeUniverse();
        }

        #endregion
    }
}
