using EternalRacer.Graph.Algorithm.Nodes;
using EternalRacer.Graph.BaseImp;
using EternalRacer.PriorityQueue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Graph.Algorithm
{
    public abstract class AAlgorithmicGraph<TVertexId> : AGraph<TVertexId>
    {
        private HashSet<Path<TVertexId>> AxClosedSet;
        private PriorityQueue<double, Path<TVertexId>> AxOpenQueue;

        public AAlgorithmicGraph()
        {
            AxClosedSet = new HashSet<Path<TVertexId>>();
            //TODO: Coś porazić na ten rozmiar:
            AxOpenQueue = new PriorityQueue<double, Path<TVertexId>>(1801);
        }

        public bool AreConnected(AAlgorithmicVertex<TVertexId> vertex1, AAlgorithmicVertex<TVertexId> vertex2)
        {
            return AxCalculatePath(vertex1, vertex2) != null;
        }

        public List<AAlgorithmicVertex<TVertexId>> FindPath(AAlgorithmicVertex<TVertexId> vertex1, AAlgorithmicVertex<TVertexId> vertex2)
        {
            Path<TVertexId> goal;
            goal = AxCalculatePath(vertex1, vertex2);

            return (goal != null) ? RetriveDirectionsByPath(goal) : new List<AAlgorithmicVertex<TVertexId>>(0);
        }

        public SearchProperties<TVertexId> DepthFirstSearch(AAlgorithmicVertex<TVertexId> rootNode, AAlgorithmicVertex<TVertexId> goal = null, Action<AAlgorithmicVertex<TVertexId>> additionalAction = null)
        {
            SearchProperties<TVertexId> properties = new SearchProperties<TVertexId>(goal);

            foreach (var vertex in Vertices)
            {
                AAlgorithmicVertex<TVertexId> algoVertex = (AAlgorithmicVertex<TVertexId>)vertex;
                algoVertex.Searching.Clear();
            }

            DfsVisit(rootNode, properties, additionalAction);

            return properties;
        }


        #region Depth First Search Implementation

        private void DfsVisit(AAlgorithmicVertex<TVertexId> vertex, SearchProperties<TVertexId> properties, Action<AAlgorithmicVertex<TVertexId>> action)
        {
            if (properties.Goal != null && properties.Goal.Equals(vertex))
            {
                properties.Found = true;
            }

            vertex.Searching.State = SearchState.Discovered;
            vertex.Searching.TimeDiscovered = ++(properties.Timer);

            foreach (AAlgorithmicVertex<TVertexId> child in vertex.AvailableVertices)
            {
                if (child.Searching.State == SearchState.Unexplored)
                {
                    vertex.Searching.Children.Add(child.Searching);
                    child.Searching.Ancestor = vertex;

                    DfsVisit(child, properties, action);
                }
            }

            vertex.Searching.State = SearchState.Explored;
            vertex.Searching.TimeExplored = ++(properties.Timer);

            if (action != null)
            {
                action(vertex);
            }

            if (!vertex.Searching.Children.Any())
            {
                properties.Leafs.Add(vertex.Searching);
            }
        }

        #endregion

        #region A* implementation

        private Path<TVertexId> AxCalculatePath(AAlgorithmicVertex<TVertexId> from, AAlgorithmicVertex<TVertexId> toThat)
        {
            AxOpenQueue.Clear();
            AxClosedSet.Clear();

            Path<TVertexId> goalNode = toThat.Pathing;

            from.Pathing.Clear();
            from.Pathing.HRecalculate(toThat);
            AxOpenQueue.Insert(from.Pathing);

            while (!AxOpenQueue.IsEmpty)
            {
                Path<TVertexId> current = AxOpenQueue.PullHighest();
                if (current.Equals(goalNode))
                {
                    return current;
                }

                AxClosedSet.Add(current);

                foreach (AAlgorithmicVertex<TVertexId> child in current.Owner.AvailableVertices)
                {
                    Path<TVertexId> successor = child.Pathing;

                    if (!AxClosedSet.Contains(successor))
                    {
                        successor.Clear();
                        successor.HRecalculate(toThat);

                        if (!AxOpenQueue.Contains(successor))
                        {
                            successor.Ancestor = current.Owner;
                            AxOpenQueue.Insert(successor);
                        }
                        else if (successor.G > current.G + current.Owner.MovementCost(successor.Owner))
                        {
                            successor.Ancestor = current.Owner;
                            AxOpenQueue.ItemPriorityChanged(successor);
                        }
                    }
                }
            }

            return null;
        }

        private List<AAlgorithmicVertex<TVertexId>> RetriveDirectionsByPath(Path<TVertexId> goal)
        {
            List<AAlgorithmicVertex<TVertexId>> setpsList = new List<AAlgorithmicVertex<TVertexId>>();
            Path<TVertexId> current = goal;

            while (current.Ancestor != null)
            {
                setpsList.Add(current.Owner);
                current = current.Ancestor.Pathing;
            }

            setpsList.Reverse();
            return setpsList;
        }

        #endregion
    }
}
