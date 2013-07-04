using EternalRacer.Pathfinding;
using System;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Strategie.Zniszczenie
{
    internal class StrategiaZniszczenia : AStrategia
    {
        private AStarPathfinder Pathfinder;

        public event EventHandler GraczeNieOsiagalni;

        public StrategiaZniszczenia(MapaGry mapaGry, Point mojaPozycjaStartowa, Point pozycjaStartowaPrzeciwnika)
            : base(mapaGry, mojaPozycjaStartowa, pozycjaStartowaPrzeciwnika)
        {
            Pathfinder = new AStarPathfinder(mapaGry.Szerokosc, mapaGry.Wysokosc, mojaPozycjaStartowa, pozycjaStartowaPrzeciwnika);
            Pathfinder.EnemySeparated += OnEnemySeparated;
        }

        private void OnEnemySeparated(object sender, EventArgs e)
        {
            ((AStarPathfinder)sender).EnemySeparated -= OnEnemySeparated;
            GraczeNieOsiagalni(this, null);
        }

        private List<Move> ListaRuchow;

        protected override Move ObliczNowyRuch()
        {
            Pathfinder.UpdatePlayersPosition(Ja.BiezacaPozycja, Przeciwnik.BiezacaPozycja);
            ListaRuchow = Pathfinder.FindPathToEnemy();

            if (ListaRuchow.Count > 0)
            {
                return ListaRuchow[0];
            }
            else
            {
                return Move.Left;
            }
        }
    }
}
