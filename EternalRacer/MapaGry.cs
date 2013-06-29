using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    internal class MapaGry
    {
        internal readonly int Szerokosc;
        internal readonly int Wysokosc;

        private StanyPola[][] mapa;

        public MapaGry(int szerokosc, int wysokosc)
        {
            Szerokosc = szerokosc;
            Wysokosc = wysokosc;

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

        public MapaGry Aktualizuj(Point ja, Point on)
        {
            mapa[ja.X][ja.Y] = StanyPola.ZajeteMoje;
            mapa[on.X][on.Y] = StanyPola.ZajeteMoje;

            return this;
        }

        public StanyPola this[Point punkt]
        {
            get { return mapa[punkt.X][punkt.Y]; }
            set { mapa[punkt.X][punkt.Y] = value; }
        }
    }
}
