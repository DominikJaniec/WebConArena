using System;
using System.AddIn;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    [AddInAttribute("Eternal",
        Version = "0.0.0.2",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        public Move GetMove(Point myPosition, Point opponentPosition, List<MapPoint> map)
        {
            throw new NotImplementedException();
        }
    }
}
