using EternalRacer.Map;
using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IVertex
    {
        IEnumerable<IVertex> Edges { get; }

        bool IsArticulationPoint { get; }
        void DetermineCutVertex();

        NodeSearching SearchingNode { get; }

        NodePathing PathingNode { get; }
        double DistanceTo(IVertex toThat);
        Directions DirectionTo(IVertex toThat);
    }
}
