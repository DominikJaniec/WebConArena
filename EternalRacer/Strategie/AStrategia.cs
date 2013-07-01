using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Strategie
{
    internal abstract class AStrategia
    {
        #region Pola protected

        protected MapaGry Mapa;

        protected StanGracza Ja;
        protected StanGracza Przeciwnik;

        #endregion

        #region Konstruktory

        public AStrategia(MapaGry mapaGry, Point mojaPozycjaStartowa, Point pozycjaStartowaPrzeciwnika)
        {
            Mapa = mapaGry;
            Ja = new StanGracza(mojaPozycjaStartowa);
            Przeciwnik = new StanGracza(pozycjaStartowaPrzeciwnika);
        }

        public AStrategia(AStrategia naPodstawie)
        {
            Mapa = naPodstawie.Mapa;
            Ja = naPodstawie.Ja;
            Przeciwnik = naPodstawie.Przeciwnik;
        }

        #endregion

        #region Metody

        public Move WykonajRuch(Point mojaBiezacaPozycja, Point biezacaPozycjaPrzeciwnika)
        {
            Mapa.Aktualizuj(mojaBiezacaPozycja, biezacaPozycjaPrzeciwnika);
            Ja.BiezacaPozycja = mojaBiezacaPozycja;
            Przeciwnik.BiezacaPozycja = biezacaPozycjaPrzeciwnika;

            return ObliczNowyRuch();
        }

        protected abstract Move ObliczNowyRuch();

        #endregion
    }
}
