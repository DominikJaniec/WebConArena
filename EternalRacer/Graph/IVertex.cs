using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IVertex
    {
        IEnumerable<IVertex> Edges { get; }

        bool IsArticulationPoint { get; }
        void DetermineCutVertex();

        NodeSearching SearchNode { get; }        
    }
}
