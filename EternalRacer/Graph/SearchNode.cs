using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public class SearchNode
    {
        public SearchState State { get; set; }

        public ISearchNodeProvider Ancestor { get; set; }
        public List<ISearchNodeProvider> Descendants { get; private set; }

        public int TimeDiscovered { get; set; }
        public int TimeExplored { get; set; }

        public SearchNode()
        {
            Descendants = new List<ISearchNodeProvider>();
            Reset();
        }

        public void Reset()
        {
            State = SearchState.Unexplored;

            Ancestor = null;
            Descendants.Clear();

            TimeDiscovered = TimeExplored = 0;
        }
    }
}
