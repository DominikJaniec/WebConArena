using EternalRacer.Graph.Nodes;
using EternalRacer.Map;
using System.Collections.Generic;

namespace EternalRacer.Graph
{
    public interface IVertex
    {
        IEnumerable<IVertex> Edges { get; }

        bool IsArticulationPoint { get; }
        void DetermineCutVertex();

        Search Searching { get; }
        Path Pathing { get; }
        Voronoi Voronoing { get; }

        double DistanceTo(IVertex toThat);
        Directions DirectionTo(IVertex toThat);
    }
}
