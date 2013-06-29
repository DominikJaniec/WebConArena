using EternalRacer.Enums;
using EternalRacer.Extensions;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer
{
    [AddInAttribute("Eternal",
        Version = "0.0.0.3",
        Description = "Wieczny Jeździec",
        Publisher = "Dominik Janiec")]
    public class Eternal : IRacer
    {
        private Interakcja mozliwoscInterakcji;
        private bool pierwszyRuch = true;

        private Move mojOstatniRuch;
        //private Move jegoOstatniRuch;

        private Point mojaOstatniaPozycja;

        private MapaGry mapaGry;

        private List<Kierunki> dozwoloneKierunki;


        public Move GetMove(Point myPosition, Point opponentPosition, List<MapPoint> map)
        {
            if (pierwszyRuch == false)
            {
                mapaGry.Aktualizuj(myPosition, opponentPosition);
                dozwoloneKierunki = mapaGry.OkreslDozwoloneKierunki(myPosition);

                switch (mozliwoscInterakcji)
                {
                    case Interakcja.Odcieci:
                        mojOstatniRuch = WypelniajPrzestrzen();
                        break;
                    case Interakcja.Walczacy:
                    default:
                        throw new NotImplementedException();
                }

                mojaOstatniaPozycja = myPosition;
                return mojOstatniRuch;
            }
            else
            {
                PrzygotujSie(map);
                mojOstatniRuch = UstalPierwszyRuch(myPosition, opponentPosition);

                //TODO 1:
                // Wymuszamy przypadek braku niebezpieczeństwa interakcji.
                mozliwoscInterakcji = Interakcja.Odcieci;
                mojaOstatniaPozycja = myPosition;

                mapaGry.Aktualizuj(myPosition, opponentPosition);
                return mojOstatniRuch;
            }            
        }

        private Move UstalPierwszyRuch(Point myPosition, Point opponentPosition)
        {
            //TODO: Poprawa wybierania pierwszego kierunku:
            if (myPosition.X == mapaGry.Min.X &&       // X O O
                myPosition.Y == mapaGry.Min.Y)         // O O O
            {                                          // O O O
                return Move.Down;
            }
            else if (myPosition.X == mapaGry.Min.X &&  // O O O
                     myPosition.Y == mapaGry.Max.Y)    // O O O
            {                                          // X O O
                return Move.Right;
            }
            else if (myPosition.X == mapaGry.Max.X &&  // O O O
                     myPosition.Y == mapaGry.Max.Y)    // O O O
            {                                          // O O X
                return Move.Up;
            }
            else if (myPosition.X == mapaGry.Max.X &&  // O O X
                     myPosition.Y == mapaGry.Min.Y)    // O O O
            {                                          // O O O
                return Move.Left;
            }
            else if (myPosition.X == mapaGry.Min.X)    // O O O
            {                                          // X O O
                return Move.Down;                      // O O O
            }
            else if (myPosition.X == mapaGry.Min.X)    // O O O
            {                                          // O O O
                return Move.Right;                     // O X O
            }
            else if (myPosition.X == mapaGry.Min.X)    // O O O
            {                                          // O O X
                return Move.Up;                        // O O O
            }
            else if (myPosition.X == mapaGry.Min.X)    // O X O
            {                                          // O O O
                return Move.Left;                      // O O O
            }
            else if (myPosition.X <= (mapaGry.Max.X - 1) / 2)
            {                                          // O O O
                return Move.Left;                      // OXO O
            }                                          // O O O
            else                                       // O O O
            {                                          // O OXO
                return Move.Right;                     // O O O
            }
        }

        private Move WypelniajPrzestrzen()
        {
            //TODO: Wypełnainie przestrzeni:
            return mojOstatniRuch.Skrec(Skrecanie.JedzProsto);
        }

        private void PrzygotujSie(List<MapPoint> map)
        {
            int szerokosc, wysokosc;
            szerokosc = map.Max(mp => mp.Point.X) + 1;
            wysokosc = map.Max(mp => mp.Point.Y) + 1;

            mapaGry = new MapaGry(szerokosc, wysokosc);

            mozliwoscInterakcji = Interakcja.Walczacy;

            pierwszyRuch = false;
        }
    }
}
