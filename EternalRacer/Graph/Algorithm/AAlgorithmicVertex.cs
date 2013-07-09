using EternalRacer.Graph.Algorithm.Nodes;
using EternalRacer.Graph.BaseImp;
using System;
using System.Linq;

namespace EternalRacer.Graph.Algorithm
{
    public abstract class AAlgorithmicVertex<TVertexId> : Vertex<TVertexId>
    {
        public Path<TVertexId> Pathing { get; private set; }
        public Search<TVertexId> Searching { get; private set; }
        public Voronoi<TVertexId> Voronoing { get; private set; }


        public AAlgorithmicVertex(AAlgorithmicGraph<TVertexId> containingGraph, TVertexId vertexId)
            : base(containingGraph, vertexId)
        {
            Pathing = new Path<TVertexId>(this);
            Searching = new Search<TVertexId>(this);
            Voronoing = new Voronoi<TVertexId>(this);
        }


        public abstract double DistanceTo(AAlgorithmicVertex<TVertexId> goalVertex);

        public double MovementCost(AAlgorithmicVertex<TVertexId> toVertex)
        {
            if (!AvailableVertices.Contains(toVertex))
            {
                throw new InvalidOperationException("Vertex toVertex is NOT in AvailableVertices.");
            }

            return Edges.Single(ve => ve.Another(this) == toVertex).Weight(this);
        }
    }
}
