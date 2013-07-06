namespace EternalRacer.Graph.Nodes
{
    public class Search : ANode
    {
        public SearchState State { get; set; }

        public int TimeDiscovered { get; set; }
        public int TimeExplored { get; set; }

        public Search(IVertex owner)
            : base(owner) { }

        public override void Clear()
        {
            base.Clear();

            State = SearchState.Unexplored;
            TimeDiscovered = 0;
            TimeExplored = 0;
        }
    }
}
