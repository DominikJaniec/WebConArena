using EternalRacer.Graph;
using EternalRacer.Graph.Algorithm;
using EternalRacer.Graph.BaseImp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EternalRacer.Game.World
{
    public class Spot : AAlgorithmicVertex<Coordinate>
    {
        private SpotState state;
        public SpotState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;

                    switch (state)
                    {
                        case SpotState.Space:
                            foreach (VertexEdge<Coordinate> vertexEdge in Edges)
                            {
                                Spot neighbour = (Spot)vertexEdge.Another(this);
                                if (neighbour.State != SpotState.Disabled)
                                {
                                    vertexEdge.ChangeConnection(neighbour, VertexEdgeConnection.Open);
                                    vertexEdge.ChangeConnection(this, VertexEdgeConnection.Open);
                                }
                            }
                            break;
                        case SpotState.Player:
                            foreach (VertexEdge<Coordinate> vertexEdge in Edges)
                            {
                                Spot neighbour = (Spot)vertexEdge.Another(this);
                                if (neighbour.State != SpotState.Disabled)
                                {
                                    vertexEdge.ChangeConnection(neighbour, VertexEdgeConnection.Closed);
                                    vertexEdge.ChangeConnection(this, VertexEdgeConnection.Open);
                                }
                            }
                            break;
                        case SpotState.Enemy:
                            foreach (VertexEdge<Coordinate> vertexEdge in Edges)
                            {
                                Spot neighbour = (Spot)vertexEdge.Another(this);
                                if (neighbour.State != SpotState.Disabled)
                                {
                                    vertexEdge.ChangeConnection(neighbour, VertexEdgeConnection.Open);
                                    vertexEdge.ChangeConnection(this, VertexEdgeConnection.Closed);
                                }
                            }
                            break;
                        case SpotState.Disabled:
                            foreach (VertexEdge<Coordinate> vertexEdge in Edges)
                            {
                                vertexEdge.ChangeConnection(vertexEdge.Another(this), VertexEdgeConnection.Closed);
                                vertexEdge.ChangeConnection(this, VertexEdgeConnection.Closed);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("State", state, "Unknown SpotState");
                    }
                }
            }
        }

        public IEnumerable<Directions> MovementDirections
        {
            get { return AvailableVertices.Cast<Spot>().Where(s => s.State == SpotState.Space).Select(s => DirectionToNeighbour(s)); }
        }

        public Spot(Universe universeMap, Coordinate spotCoordinate)
            : base(universeMap, spotCoordinate) { }

        public override double DistanceTo(AAlgorithmicVertex<Coordinate> goalVertex)
        {
            if (goalVertex == null)
            {
                throw new ArgumentNullException("goalVertex");
            }

            int dx = Id.X - goalVertex.Id.X;
            dx = (dx < 0) ? (-1) * dx : dx;

            int dy = Id.Y - goalVertex.Id.Y;
            dy = (dy < 0) ? (-1) * dy : dy;

            return (dx + dy);
        }

        public Directions DirectionToNeighbour(Spot neighbour)
        {
            if (neighbour == null)
            {
                throw new ArgumentNullException("neighbour");
            }

            int dx = neighbour.Id.X - Id.X;
            int dy = neighbour.Id.Y - Id.Y;

            if (dx == 0 && dy == -1)
            {
                return Directions.North;
            }
            else if (dx == 1 && dy == 0)
            {
                return Directions.East;
            }
            else if (dx == 0 && dy == 1)
            {
                return Directions.South;
            }
            else if (dx == -1 && dy == 0)
            {
                return Directions.West;
            }
            else
            {
                throw new ArgumentOutOfRangeException("neighbour", neighbour, "It is in an unknown direction.");
            }
        }

        public void BindWithNeighbours()
        {
            // Directions.North:
            BindWithNeighboursIfExistInGraph(Id.X, Id.Y - 1);
            // Directions.East:
            BindWithNeighboursIfExistInGraph(Id.X + 1, Id.Y);
            // Directions.South:
            BindWithNeighboursIfExistInGraph(Id.X, Id.Y + 1);
            // Directions.West:
            BindWithNeighboursIfExistInGraph(Id.X - 1, Id.Y);
        }
        private void BindWithNeighboursIfExistInGraph(int nX, int nY)
        {
            Coordinate neighbourCoordinate = new Coordinate(nX, nY);
            if (Graph.IsValidId(neighbourCoordinate))
            {
                Spot neighbour = ((Universe)Graph)[neighbourCoordinate];
                if (!AvailableVertices.Contains(neighbour))
                {
                    VertexEdge<Coordinate>.BindTogether(this, neighbour);
                }
            }
        }
    }
}