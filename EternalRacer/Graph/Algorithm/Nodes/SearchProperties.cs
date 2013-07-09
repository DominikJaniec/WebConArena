using System.Collections.Generic;

namespace EternalRacer.Graph.Algorithm.Nodes
{
    public class SearchProperties<TVertexId>
    {
        public int Timer { get; set; }

        public AAlgorithmicVertex<TVertexId> Goal { get; set; }
        public bool Found { get; set; }

        public List<Search<TVertexId>> Leafs { get; set; }

        public SearchProperties(AAlgorithmicVertex<TVertexId> goal)
        {
            Timer = 0;

            Goal = goal;
            Found = false;

            Leafs = new List<Search<TVertexId>>();
        }
    }
}
