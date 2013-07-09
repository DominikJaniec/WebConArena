using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EternalRacer.Graph.BaseImp
{
    public class Vertex<TVertexId> :
        IVertex<TVertexId, double, Vertex<TVertexId>, VertexEdge<TVertexId>>,
        IEquatable<Vertex<TVertexId>>
    {
        public static int Possible_Number_Of_Edges_For_New_Vertex = 4;


        public TVertexId Id { get; private set; }
        public AGraph<TVertexId> Graph { get; private set; }

        public List<VertexEdge<TVertexId>> Edges { get; private set; }
        public IEnumerable<Vertex<TVertexId>> AvailableVertices
        {
            get { return Edges.Where(ve => ve.Connection(this) == VertexEdgeConnection.Open).Select(ve => ve.Another(this)); }
        }


        public Vertex(AGraph<TVertexId> containingGraph, TVertexId vertexId)
        {
            if (containingGraph == null)
            {
                throw new ArgumentNullException("graph");
            }

            if (vertexId == null)
            {
                throw new ArgumentNullException("vertexId");
            }

            Id = vertexId;
            Graph = containingGraph;

            Edges = new List<VertexEdge<TVertexId>>(Possible_Number_Of_Edges_For_New_Vertex);
        }


        public static bool operator ==(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if (ReferenceEquals(vertex1, vertex2))
            {
                return true;
            }

            if (Equals(vertex1, null) || Equals(vertex2, null))
            {
                return false;
            }

            return vertex1.Id.Equals(vertex2.Id);
        }
        public static bool operator !=(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            return !(vertex1 == vertex2);
        }

        public bool Equals(Vertex<TVertexId> other)
        {
            return this == other;
        }
        public override bool Equals(Object obj)
        {
            return obj != null && Equals(obj as Vertex<TVertexId>);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendFormat("{0}:", Id);

            foreach (VertexEdge<TVertexId> edge in Edges)
            {
                strBuilder.AppendFormat(" {0} |", edge.ToString(this));
            }

            return strBuilder.ToString();
        }
    }
}