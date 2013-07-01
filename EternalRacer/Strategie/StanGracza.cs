using System;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Strategie
{
    internal class StanGracza
    {
        #region Publiczne powiązane własności

        private Point ostatniaPozycja;
        public Point OstatniaPozycja
        {
            get { return ostatniaPozycja; }
        }

        private Point biezacaPozycja;
        public Point BiezacaPozycja
        {
            get { return biezacaPozycja; }
            set
            {
                if (WykonanoRuch(biezacaPozycja, value) == true)
                {
                    ostatniaPozycja = biezacaPozycja;
                    biezacaPozycja = value;

                    wykonanyRuch = ObliczWykonanyRuch(ostatniaPozycja, biezacaPozycja);
                }
            }
        }

        private Move wykonanyRuch;
        public Move WykonanyRuch
        {
            get { return wykonanyRuch; }
        }

        #endregion

        #region Konstruktor

        public StanGracza(Point pozycjaStartowa)
        {
            ostatniaPozycja = new Point { X = -1, Y = -1 };
            biezacaPozycja = pozycjaStartowa;
            wykonanyRuch = (Move)99;
        }

        #endregion

        #region Publiczne metody statyczne

        public static Move ObliczWykonanyRuch(Point ostatniaPozycja, Point biezacaPozycja)
        {
            int deltaX = ostatniaPozycja.X - biezacaPozycja.X;
            int deltaY = ostatniaPozycja.Y - biezacaPozycja.Y;

            if (deltaY == 0)
            {
                if (deltaX == 1)
                {
                    return Move.Right;
                }
                else if (deltaX == -1)
                {
                    return Move.Left;
                }
                else
                {
                    throw new InvalidOperationException("Nieprawidłowy ruch w Osi X");
                }
            }
            else if (deltaX == 0)
            {
                if (deltaY == 1)
                {
                    return Move.Down;
                }
                else if (deltaY == -1)
                {
                    return Move.Up;
                }
                else
                {
                    throw new InvalidOperationException("Nieprawidłowy ruch w Osi Y");
                }
            }
            else
            {
                throw new InvalidOperationException("Nieprawidłowy ruch w Osiach X, Y");
            }
        }

        public static bool WykonanoRuch(Point ostatniaPozycja, Point biezacaPozycja)
        {
            return ostatniaPozycja.X != biezacaPozycja.X || ostatniaPozycja.Y != biezacaPozycja.Y;
        }

        #endregion
    }
}
