using System.Collections.Generic;

namespace EternalRacer.Graph.Algorithm.Nodes
{
    public class Search<TVertexId> : ANode<TVertexId>
    {
        public SearchState State { get; set; }

        public int TimeDiscovered { get; set; }
        public int TimeExplored { get; set; }

        public List<Search<TVertexId>> Children { get; private set; }

        public Search(AAlgorithmicVertex<TVertexId> owner)
            : base(owner)
        {
            Children = new List<Search<TVertexId>>();
        }

        public override void Clear()
        {
            base.Clear();

            State = SearchState.Unexplored;
            TimeDiscovered = 0;
            TimeExplored = 0;

            Children.Clear();
        }
    }
}
