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
        Version = "0.0.1.5",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        #region Metoda "GetMove" z IRacer

        public Move GetMove(Point myPosition, Point opponentPosition, List<MapPoint> map)
        {
            return GetMove_Funkcja(myPosition, opponentPosition, map);
        }

        #endregion

        #region Implementacja metody "GetMove"

        private Func<Point, Point, List<MapPoint>, Move> GetMove_Funkcja;

        private Move GetMove_PodczasGrzy(Point mojaBiezacaPozycja, Point jegoBiezacaPozycja, List<MapPoint> mapa_nieWykozystywana)
        {
            return Strategia.WykonajRuch(mojaBiezacaPozycja, jegoBiezacaPozycja);
        }

        private Move GetMove_PierwszyRaz(Point mojaStartowa, Point jegoStartowa, List<MapPoint> mapaGry)
        {
            int szerokosc = mapaGry.Max(mp => mp.Point.X) + 1;
            int wysokosc = mapaGry.Max(mp => mp.Point.Y) + 1;
            Mapa = new MapaGry(szerokosc, wysokosc);

            StrategiaZniszczenia strategiaZniszczenia = new StrategiaZniszczenia(Mapa, mojaStartowa, jegoStartowa);
            strategiaZniszczenia.GraczeNieOsiagalni += OnGraczeNieOsiagalni;
            Strategia = strategiaZniszczenia;

            GetMove_Funkcja = GetMove_PodczasGrzy;
            return GetMove_Funkcja(mojaStartowa, jegoStartowa, mapaGry);
        }

        #endregion


        #region Publiczne Własności

        public MapaGry Mapa { get; private set; }

        internal AStrategia Strategia { get; set; }
        public RodzajeStrategii BiezacaStrategia { get; internal set; }

        #endregion


        #region Konstruktor i prywatna zmiana Strategii

        public Eternal()
        {
            GetMove_Funkcja = GetMove_PierwszyRaz;
        }

        private void OnGraczeNieOsiagalni(object sender, EventArgs e)
        {
            StrategiaZniszczenia staraStrategiaZniszczenia = (StrategiaZniszczenia)sender;
            staraStrategiaZniszczenia.GraczeNieOsiagalni -= OnGraczeNieOsiagalni;

            Strategia = new StrategiaPrzetrwania(staraStrategiaZniszczenia);
        }

        #endregion
    }
}
