using System;
using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public class GraphAlgorithms<TGraph, TVertex>
        where TGraph : IGraph<TVertex>
        where TVertex : IVertex
    {
        private readonly TGraph Graph;

        public GraphAlgorithms(TGraph graph)
        {
            Graph = graph;
            DfsLeafs = new List<TVertex>();
        }

        private int DfsTimer;
        private List<TVertex> DfsLeafs;

        public List<TVertex> DepthFirstSearch(TVertex rootNode)
        {
            foreach (TVertex vertex in Graph.GetVertices())
            {
                vertex.SearchNode.Reset();
            }

            DfsTimer = 0;
            DfsLeafs.Clear();

            DfsVisit(rootNode);

            return DfsLeafs;
        }

        private void DfsVisit(TVertex vertex)
        {
            bool hasUnexploredChildren = false;

            vertex.SearchNode.State = SearchState.Discovered;
            vertex.SearchNode.TimeDiscovered = ++DfsTimer;

            foreach (TVertex child in vertex.Edges)
            {
                if (child.SearchNode.State == SearchState.Unexplored)
                {
                    child.SearchNode.Ancestor = vertex;
                    hasUnexploredChildren = true;

                    DfsVisit(child);
                }
            }

            vertex.SearchNode.State = SearchState.Explored;
            vertex.SearchNode.TimeExplored = ++DfsTimer;

            if (!hasUnexploredChildren)
            {
                DfsLeafs.Add(vertex);
            }
        }

        internal bool Connected(TVertex vertexFirst, TVertex vertexSecond)
        {
            throw new NotImplementedException();
        }
    }
}
