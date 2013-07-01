using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    public class MapaGry
    {
        #region Publiczne pola i własności

        public readonly int Szerokosc;
        public readonly int Wysokosc;

        public readonly Point Max;
        public readonly Point Min = new Point
        {
            X = 0,
            Y = 0
        };

        #endregion

        #region Prywantne pole

        private StanyPola[][] mapa;

        #endregion

        #region Konstruktor

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

        #endregion

        #region Publiczne metody

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

        #endregion
    }
}
