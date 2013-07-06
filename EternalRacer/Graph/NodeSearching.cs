namespace EternalRacer.Graph
{
    public class NodeSearching
    {
        public SearchState State { get; set; }
        public IVertex Ancestor { get; set; }

        public int TimeDiscovered { get; set; }
        public int TimeExplored { get; set; }

        public void Reset()
        {
            Ancestor = null;
            State = SearchState.Unexplored;
            TimeDiscovered = TimeExplored = 0;
        }
    }
}
