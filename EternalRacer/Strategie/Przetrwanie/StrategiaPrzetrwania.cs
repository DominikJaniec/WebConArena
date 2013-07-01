using System;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Strategie.Przetrwanie
{
    internal class StrategiaPrzetrwania : AStrategia
    {
        public StrategiaPrzetrwania(AStrategia naPodstawie)
            : base(naPodstawie) { }

        protected override Move ObliczNowyRuch()
        {
            //TODO 1: Implementacja wypełniania dostepnego świata:
            throw new NotImplementedException();
        }
    }
}
