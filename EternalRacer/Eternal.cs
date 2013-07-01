using EternalRacer.Strategie;
using EternalRacer.Strategie.Przetrwanie;
using EternalRacer.Strategie.Zniszczenie;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    [AddInAttribute("Eternal",
        Version = "0.0.1.2",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        #region Metoda z IRacer

        public Move GetMove(Point myPosition, Point opponentPosition, List<MapPoint> map)
        {
            return ImplementacjaGetMove(myPosition, opponentPosition, map);
        }

        #endregion

        #region Publiczne Własności

        public MapaGry Mapa { get; private set; }

        internal AStrategia Strategia { get; set; }
        public RodzajeStrategii BiezacaStrategia { get; internal set; }

        #endregion

        #region Konstruktor

        public Eternal()
        {
            ImplementacjaGetMove = new Func<Point, Point, List<MapPoint>, Move>((mojaStartowa, jegoStartowa, mapaGry) =>
            {
                ImplementacjaGetMove = (mojaPozycja, jegoPozycja, mapa_zbedna) =>
                {
                    return Strategia.WykonajRuch(mojaPozycja, jegoPozycja);
                };

                int szerokosc = mapaGry.Max(mp => mp.Point.X) + 1;
                int wysokosc = mapaGry.Max(mp => mp.Point.Y) + 1;
                Mapa = new MapaGry(szerokosc, wysokosc);

                StrategiaZniszczenia strategiaZniszczenia = new StrategiaZniszczenia(Mapa, mojaStartowa, jegoStartowa);
                strategiaZniszczenia.GraczeNieOsiagalni += OnGraczeNieOsiagalni;
                Strategia = strategiaZniszczenia;

                return ImplementacjaGetMove(mojaStartowa, jegoStartowa, mapaGry);
            });
        }

        #endregion

        #region Prywatne

        private Func<Point, Point, List<MapPoint>, Move> ImplementacjaGetMove;

        private void OnGraczeNieOsiagalni(object sender, EventArgs e)
        {
            StrategiaZniszczenia staraStrategiaZniszczenia = (StrategiaZniszczenia)sender;
            staraStrategiaZniszczenia.GraczeNieOsiagalni -= OnGraczeNieOsiagalni;

            Strategia = new StrategiaPrzetrwania(staraStrategiaZniszczenia);
        }

        #endregion
    }
}
