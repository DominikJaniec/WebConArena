using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IVertex<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
        where TVertexImp : IVertex<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
        where TEdgeImp : IVertexEdge<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
    {
        TVertexId Id { get; }

        List<TEdgeImp> Edges { get; }
        IEnumerable<TVertexImp> AvailableVertices { get; }
    }
}
