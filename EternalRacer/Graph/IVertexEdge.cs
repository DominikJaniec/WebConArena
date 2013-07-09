
namespace EternalRacer.Graph
{
    public interface IVertexEdge<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
        where TVertexImp : IVertex<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
        where TEdgeImp : IVertexEdge<TVertexId, TEdgeWeight, TVertexImp, TEdgeImp>
    {
        bool IsUndirected { get; }

        TVertexImp Another(TVertexImp fromVertex);
        VertexEdgeConnection Connection(TVertexImp fromVertex);
        TEdgeWeight Weight(TVertexImp fromVertex);
    }
}
