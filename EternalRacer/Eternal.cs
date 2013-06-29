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
            int kierunek = R.Next(4);

            switch (kierunek)
            {
                case 0:
                    return Move.Down;
                case 1:
                    return Move.Left;
                case 2:
                    return Move.Right;
                case 3:
                    return Move.Up;
                default:
                    throw new InvalidOperationException("kierunek jest poza zakresem liczbowym...");
            }
        }

        // Ważna data dla mnie :)
        private Random R = new Random(new DateTime(2010, 03, 15).DayOfYear+1);
    }
}
