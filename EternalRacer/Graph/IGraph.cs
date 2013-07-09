using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IGraph<TVertexId, TEdgeWeight, TGraphImp, TVertexImp, TEdgeImp>
        where TGraphImp : IGraph<TVertexId, TEdgeWeight, TGraphImp, TVertexImp, TEdgeImp>
        where TVertexImp : IVertex<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
        where TEdgeImp : IVertexEdge<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
    {
        IEnumerable<TVertexImp> Vertices { get; }
        bool IsValidId(TVertexId vertexId);
    }
}
