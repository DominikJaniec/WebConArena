using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IGraph<T> where T : IVertex
    {
        IEnumerable<T> GetVertices();
    }
}
