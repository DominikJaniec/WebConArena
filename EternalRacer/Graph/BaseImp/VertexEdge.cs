using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Graph.BaseImp
{
    public class VertexEdge<TVertexId> :
        IVertexEdge<TVertexId, double, Vertex<TVertexId>, VertexEdge<TVertexId>>
    {
        private Dictionary<Vertex<TVertexId>, VertexEdgeConnection> verticesConnectionToAnother;

        public bool IsUndirected
        {
            get { return verticesConnectionToAnother.Values.All(c => c == VertexEdgeConnection.Open); }
        }

        public Vertex<TVertexId> Another(Vertex<TVertexId> fromVertex)
        {
            return verticesConnectionToAnother.Single(vk => vk.Key != fromVertex).Key;
        }

        public VertexEdgeConnection Connection(Vertex<TVertexId> fromVertex)
        {
            return verticesConnectionToAnother[fromVertex];
        }
        public void ChangeConnection(Vertex<TVertexId> fromVertex, VertexEdgeConnection newConnection)
        {
            if (verticesConnectionToAnother[fromVertex] != newConnection)
            {
                verticesConnectionToAnother[fromVertex] = newConnection;
            }
        }

        private double connectionWeight;
        public double Weight(Vertex<TVertexId> fromVertex)
        {
            return connectionWeight;
        }

        private VertexEdge()
        {
            verticesConnectionToAnother = new Dictionary<Vertex<TVertexId>, VertexEdgeConnection>(2);
        }
        public static void BindTogether(Vertex<TVertexId> vertexA, Vertex<TVertexId> vertexB, double weight = 1.0)
        {
            if (vertexA == null)
            {
                throw new ArgumentNullException("vertexA");
            }

            if (vertexB == null)
            {
                throw new ArgumentNullException("vertexB");
            }

            VertexEdge<TVertexId> newEdge = new VertexEdge<TVertexId>();

            newEdge.verticesConnectionToAnother.Add(vertexA, VertexEdgeConnection.Open);
            newEdge.verticesConnectionToAnother.Add(vertexB, VertexEdgeConnection.Open);

            newEdge.connectionWeight = weight;

            vertexA.Edges.Add(newEdge);
            vertexB.Edges.Add(newEdge);
        }

        public string ToString(Vertex<TVertexId> fromVertex)
        {
            string state = (verticesConnectionToAnother[fromVertex] == VertexEdgeConnection.Open) ? "->" : "X ";
            return String.Format("-{0} {1} ({2})", state, Another(fromVertex).Id, connectionWeight);
        }
        public override string ToString()
        {
            List<Vertex<TVertexId>> verticesList = verticesConnectionToAnother.Select(kvp => kvp.Key).ToList();
            return String.Format("{0} - {1}", verticesList[0].Id, verticesList[1].Id);
        }
    }
}