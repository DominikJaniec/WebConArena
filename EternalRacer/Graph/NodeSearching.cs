namespace EternalRacer.Graph
{
    public class NodeSearching
    {
        public IVertex Ancestor { get; set; }
        public SearchState State { get; set; }

        public int TimeDiscovered { get; set; }
        public int TimeExplored { get; set; }

        public NodeSearching()
        {
            Clear();
        }

        public void Clear()
        {
            Ancestor = null;
            State = SearchState.Unexplored;

            TimeDiscovered = 0;
            TimeExplored = 0;
        }
    }
}
