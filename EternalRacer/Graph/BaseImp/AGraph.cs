using System.Collections.Generic;

namespace EternalRacer.Graph.BaseImp
{
    public abstract class AGraph<TVertexId> :
       IGraph<TVertexId, double, AGraph<TVertexId>, Vertex<TVertexId>, VertexEdge<TVertexId>>
    {
        public abstract IEnumerable<Vertex<TVertexId>> Vertices { get; }
        public abstract bool IsValidId(TVertexId vertexId);
    }
}
