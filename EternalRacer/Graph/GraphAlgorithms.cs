using EternalRacer.Graph.Nodes;
using EternalRacer.Map;
using EternalRacer.PriorityQueue;
using System.Collections.Generic;
using System.Linq;

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
            AxClosedSet = new HashSet<Path>();
            AxOpenQueue = new PriorityQueue<double, Path>(1801);
        }


        public List<TVertex> DepthFirstSearch(TVertex rootNode)
        {
            foreach (TVertex vertex in Graph.GetVertices())
            {
                vertex.Searching.Clear();
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
            Path goal = AxCalculatePath(fromThis, toThat);
            return (goal != null) ? RetriveDirectionsByPath(goal) : new List<Directions>(0);
        }

        #region Depth First Search Implementation

        private int DfsTimer;
        private List<TVertex> DfsLeafs;

        private void DfsVisit(TVertex vertex)
        {
            bool hasUnexploredChildren = false;

            vertex.Searching.State = SearchState.Discovered;
            vertex.Searching.TimeDiscovered = ++DfsTimer;

            foreach (TVertex child in vertex.Edges)
            {
                if (child.Searching.State == SearchState.Unexplored)
                {
                    child.Searching.Ancestor = vertex;
                    hasUnexploredChildren = true;

                    DfsVisit(child);
                }
            }

            vertex.Searching.State = SearchState.Explored;
            vertex.Searching.TimeExplored = ++DfsTimer;

            if (!hasUnexploredChildren)
            {
                DfsLeafs.Add(vertex);
            }
        }

        #endregion

        #region A* implementation

        private PriorityQueue<double, Path> AxOpenQueue;
        private HashSet<Path> AxClosedSet;

        private Path AxCalculatePath(TVertex from, TVertex toThat)
        {
            AxOpenQueue.Clear();
            AxClosedSet.Clear();

            Path goalNode = toThat.Pathing;

            from.Pathing.Clear();
            from.Pathing.H = from.DistanceTo(toThat);
            AxOpenQueue.Insert(from.Pathing);

            while (!AxOpenQueue.IsEmpty)
            {
                Path current = AxOpenQueue.PullHighest();
                if (current.Equals(goalNode))
                {
                    return current;
                }

                AxClosedSet.Add(current);

                foreach (Path successor in current.Owner.Edges.Select(iv => iv.Pathing))
                {
                    if (!AxClosedSet.Contains(successor))
                    {
                        successor.Clear();
                        successor.H = current.Owner.DistanceTo(goalNode.Owner);

                        if (!AxOpenQueue.Contains(successor))
                        {
                            successor.Ancestor = current.Owner;
                            AxOpenQueue.Insert(successor);
                        }
                        else if (successor.G > current.G + current.MovmentCost)
                        {
                            successor.Ancestor = current.Owner;
                            AxOpenQueue.ItemPriorityChanged(successor);
                        }
                    }
                }
            }

            return null;
        }

        private List<Directions> RetriveDirectionsByPath(Path goal)
        {
            List<Directions> movmentsList = new List<Directions>();
            Path current = goal;

            while (current.Ancestor != null)
            {
                movmentsList.Add(current.Owner.DirectionTo(current.Ancestor));
                current = current.Ancestor.Pathing;
            }

            return movmentsList;
        }

        #endregion
    }
}
