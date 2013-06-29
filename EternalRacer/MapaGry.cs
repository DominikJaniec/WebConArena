using EternalRacer.Enums;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    internal class MapaGry
    {
        public readonly int Szerokosc;
        public readonly int Wysokosc;

        public readonly Point Max;
        public readonly Point Min = new Point
        {
            X = 0,
            Y = 0
        };

        private StanyPola[][] mapa;

        public MapaGry(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;

            Max = new Point
            {
                X = szerokosc - 1,
                Y = wysokosc - 1
            };

            mapa = new StanyPola[Szerokosc][];
            for (int i = 0; i < Szerokosc; i++)
            {
                mapa[i] = new StanyPola[Wysokosc];
                for (int j = 0; j < Wysokosc; j++)
                {
                    mapa[i][j] = StanyPola.Wolne;
                }
            }
        }

        public void Aktualizuj(Point ja, Point on)
        {
            mapa[ja.X][ja.Y] = StanyPola.ZajeteMoje;
            mapa[on.X][on.Y] = StanyPola.ZajeteMoje;
        }

        public StanyPola this[Point punkt]
        {
            get { return mapa[punkt.X][punkt.Y]; }
            set { mapa[punkt.X][punkt.Y] = value; }
        }

        public List<Kierunki> OkreslDozwoloneKierunki(Point sKad)
        {
            List<Kierunki> kierunki = new List<Kierunki>(8);
            Kierunki badanyKierunek;

            badanyKierunek = Kierunki.Gora;
            if (sKad.Y > Min.Y && mapa[sKad.X][sKad.Y - 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.GoraLewo;
            if (sKad.X > Min.X && sKad.Y > Min.Y && mapa[sKad.X - 1][sKad.Y - 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.Lewo;
            if (sKad.X > Min.X && mapa[sKad.X - 1][sKad.Y] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.DolLewo;
            if (sKad.X > Min.X && sKad.Y < Max.Y && mapa[sKad.X - 1][sKad.Y + 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.Dol;
            if (sKad.Y < Max.Y && mapa[sKad.X][sKad.Y + 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.DolPrawo;
            if (sKad.X < Max.X && sKad.Y < Max.Y && mapa[sKad.X + 1][sKad.Y + 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.Prawo;
            if (sKad.X < Max.X && mapa[sKad.X + 1][sKad.Y] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            badanyKierunek = Kierunki.GoraPrawo;
            if (sKad.X < Max.X && sKad.Y > Min.Y && mapa[sKad.X + 1][sKad.Y - 1] == StanyPola.Wolne)
            {
                kierunki.Add(badanyKierunek);
            }

            return kierunki;
        }
    }
}
