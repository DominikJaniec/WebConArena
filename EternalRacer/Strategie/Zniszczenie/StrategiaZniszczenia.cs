using System;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Strategie.Zniszczenie
{
    internal class StrategiaZniszczenia : AStrategia
    {
        public event EventHandler GraczeNieOsiagalni;

        public StrategiaZniszczenia(MapaGry mapaGry, Point mojaPozycjaStartowa, Point pozycjaStartowaPrzeciwnika)
            : base(mapaGry, mojaPozycjaStartowa, pozycjaStartowaPrzeciwnika) { }

        protected override Move ObliczNowyRuch()
        {
            //TODO 1: Wymuszneie tylko trybu przetrwania:
            Move pierwszyRuch = UstalPierwszyRuch();
            GraczeNieOsiagalni(this, null);
            return pierwszyRuch;
        }

        private Move UstalPierwszyRuch()
        {
            //TODO: Poprawa wybierania pierwszego kierunku ataku:
            if (Ja.BiezacaPozycja.X == Mapa.Min.X &&       // X O O
                Ja.BiezacaPozycja.Y == Mapa.Min.Y)         // O O O
            {                                              // O O O
                return Move.Down;
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Min.X &&  // O O O
                     Ja.BiezacaPozycja.Y == Mapa.Max.Y)    // O O O
            {                                              // X O O
                return Move.Right;
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Max.X &&  // O O O
                     Ja.BiezacaPozycja.Y == Mapa.Max.Y)    // O O O
            {                                              // O O X
                return Move.Up;
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Max.X &&  // O O X
                     Ja.BiezacaPozycja.Y == Mapa.Min.Y)    // O O O
            {                                              // O O O
                return Move.Left;
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Min.X)    // O O O
            {                                              // X O O
                return Move.Down;                          // O O O
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Min.X)    // O O O
            {                                              // O O O
                return Move.Right;                         // O X O
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Min.X)    // O O O
            {                                              // O O X
                return Move.Up;                            // O O O
            }
            else if (Ja.BiezacaPozycja.X == Mapa.Min.X)    // O X O
            {                                              // O O O
                return Move.Left;                          // O O O
            }
            else if (Ja.BiezacaPozycja.X <= (Mapa.Max.X - 1) / 2)
            {                                              // O O O
                return Move.Left;                          // OXO O
            }                                              // O O O
            else                                           // O O O
            {                                              // O OXO
                return Move.Right;                         // O O O
            }
        }
    }
}
