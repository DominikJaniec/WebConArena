using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    public class MapaGry
    {
        #region Publiczne pola i własności

        public readonly int Szerokosc;
        public readonly int Wysokosc;

        public readonly Point Max;
        public readonly Point Min;

        #endregion

        #region Prywantne pole

        private StanyPola[][] mapa;

        #endregion

        #region Konstruktor

        public MapaGry(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;

            Min = new Point
            {
                X = 0,
                Y = 0
            };
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
            mapa[ja.X + Min.X][ja.Y + Min.Y] = StanyPola.ZajeteMoje;
            mapa[on.X + Min.X][on.Y + Min.Y] = StanyPola.ZajeteJego;
        }

        public bool PoleNiedostepne(Point punkt)
        {
            return PoleNiedostepne(punkt.X, punkt.Y);
        }

        private bool PoleNiedostepne(int X, int Y)
        {
            return this[X, Y] != StanyPola.Wolne;
        }

        #region Indeksery

        public StanyPola this[Point punkt]
        {
            get
            {
                return this[punkt.X, punkt.Y];
            }
        }

        public StanyPola this[int X, int Y]
        {
            get
            {
                if (X >= Min.X && X <= Max.X &&
                    Y >= Min.Y && Y <= Max.Y)
                {
                    return mapa[X + Min.X][Y + Min.Y];
                }
                else
                {
                    return StanyPola.PozaSwiatem;
                }
            }
        }

        #endregion

        #endregion
    }
}
