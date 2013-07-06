using EternalRacer.PriorityQueue;
using System;
using System.Linq;
using System.Collections.Generic;
using EternalRacer.Map;

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

            //TODO: Coś porazić na ten rozmiar:
            AxClosedSet = new HashSet<NodePathing>();
            AxOpenQueue = new PriorityQueue<double, NodePathing>(1801);
        }


        public List<TVertex> DepthFirstSearch(TVertex rootNode)
        {
            foreach (TVertex vertex in Graph.GetVertices())
            {
                vertex.SearchingNode.Clear();
            }

            DfsTimer = 0;
            DfsLeafs.Clear();

            DfsVisit(rootNode);

            return DfsLeafs;
        }

        public bool Connected(TVertex first, TVertex second)
        {
            return AxCalculatePath(first, second) != null;
        }

        public List<Directions> FindPath(TVertex fromThis, TVertex toThat)
        {
            NodePathing goal = AxCalculatePath(fromThis, toThat);
            return (goal != null) ? RetriveDirectionsByPath(goal) : new List<Directions>(0);
        }

        #region Depth First Search Implementation

        private int DfsTimer;
        private List<TVertex> DfsLeafs;

        private void DfsVisit(TVertex vertex)
        {
            bool hasUnexploredChildren = false;

            vertex.SearchingNode.State = SearchState.Discovered;
            vertex.SearchingNode.TimeDiscovered = ++DfsTimer;

            foreach (TVertex child in vertex.Edges)
            {
                if (child.SearchingNode.State == SearchState.Unexplored)
                {
                    child.SearchingNode.Ancestor = vertex;
                    hasUnexploredChildren = true;

                    DfsVisit(child);
                }
            }

            vertex.SearchingNode.State = SearchState.Explored;
            vertex.SearchingNode.TimeExplored = ++DfsTimer;

            if (!hasUnexploredChildren)
            {
                DfsLeafs.Add(vertex);
            }
        }

        #endregion

        #region A* implementation

        private PriorityQueue<double, NodePathing> AxOpenQueue;
        private HashSet<NodePathing> AxClosedSet;

        private NodePathing AxCalculatePath(TVertex from, TVertex toThat)
        {
            AxOpenQueue.Clear();
            AxClosedSet.Clear();

            NodePathing goalNode = toThat.PathingNode;

            from.PathingNode.ClearAndSet(from.DistanceTo(toThat));
            AxOpenQueue.Insert(from.PathingNode);

            while (!AxOpenQueue.IsEmpty)
            {
                NodePathing current = AxOpenQueue.PullHighest();
                if (current.Equals(goalNode))
                {
                    return current;
                }

                AxClosedSet.Add(current);

                foreach (NodePathing successor in current.Current.Edges.Select(iv => iv.PathingNode))
                {
                    if (!AxClosedSet.Contains(successor))
                    {
                        if (!AxOpenQueue.Contains(successor))
                        {
                            successor.Ancestor = current.Current;
                            AxOpenQueue.Insert(successor);
                        }
                        else if (successor.G > current.G + current.MovmentCost)
                        {
                            successor.Ancestor = current.Current;
                            AxOpenQueue.ItemPriorityChanged(successor);
                        }
                    }
                }
            }

            return null;
        }

        private List<Directions> RetriveDirectionsByPath(NodePathing goal)
        {
            List<Directions> movmentsList = new List<Directions>();
            NodePathing current = goal;

            while (current.Ancestor != null)
            {
                movmentsList.Add(current.Current.DirectionTo(current.Ancestor));
                current = current.Ancestor.PathingNode;
            }

            return movmentsList;
        }

        #endregion
    }
}
