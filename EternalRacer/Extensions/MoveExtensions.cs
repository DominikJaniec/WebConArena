using EternalRacer.Enums;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Extensions
{
    internal static class MoveExtensions
    {
        public static Move Skrec(this Move jadeW, Skrecanie skrecW)
        {
            switch (jadeW)
            {
                case Move.Down:
                    switch (skrecW)
                    {
                        case Skrecanie.JedzProsto:
                            return Move.Down;
                        case Skrecanie.SkrecWLewo:
                            return Move.Right;
                        case Skrecanie.SkrecWPrawo:
                            return Move.Left;
                        default:
                            throw new System.ArgumentOutOfRangeException("skrecW");
                    }
                case Move.Left:
                    switch (skrecW)
                    {
                        case Skrecanie.JedzProsto:
                            return Move.Left;
                        case Skrecanie.SkrecWLewo:
                            return Move.Down;
                        case Skrecanie.SkrecWPrawo:
                            return Move.Up;
                        default:
                            throw new System.ArgumentOutOfRangeException("skrecW");
                    }
                case Move.Right:
                    switch (skrecW)
                    {
                        case Skrecanie.JedzProsto:
                            return Move.Right;
                        case Skrecanie.SkrecWLewo:
                            return Move.Up;
                        case Skrecanie.SkrecWPrawo:
                            return Move.Down;
                        default:
                            throw new System.ArgumentOutOfRangeException("skrecW");
                    }
                case Move.Up:
                    switch (skrecW)
                    {
                        case Skrecanie.JedzProsto:
                            return Move.Up;
                        case Skrecanie.SkrecWLewo:
                            return Move.Left;
                        case Skrecanie.SkrecWPrawo:
                            return Move.Right;
                        default:
                            throw new System.ArgumentOutOfRangeException("skrecW");
                    }
                default:
                    throw new System.ArgumentOutOfRangeException("jadeW");
            }
        }
    }
}
