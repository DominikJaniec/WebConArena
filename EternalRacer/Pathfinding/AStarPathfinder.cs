using EternalRacer.PriorityQueue;
using System;
using System.Collections.Generic;
using WebCon.Arena.Bots.AddIn;

namespace EternalRacer.Pathfinding
{
    public class AStarPathfinder
    {
        #region Grid of PathNode

        private readonly int GridSize;
        private readonly int GridWidth;
        private readonly int GridHeight;
        private PathNode[] WorldGrid;

        private int PositionToIndex(int x, int y)
        {
            return (y * GridWidth) + x;
        }

        private int PointToIndex(Point point)
        {
            return (point.Y * GridWidth) + point.X;
        }

        #endregion

        #region Players position

        private Point PlayerPosition = new Point { X = -1, Y = -1 };

        private Point EnemyPosition = new Point { X = -1, Y = -1 };
        private Point ProbableEnemyPosition = new Point { X = -1, Y = -1 };

        public void UpdatePlayersPosition(Point player, Point enemy)
        {
            #region Argument validation

            if (player.X < 0 || player.X >= GridWidth ||
                player.Y < 0 || player.Y >= GridHeight)
            {
                throw new ArgumentOutOfRangeException("player", player, "Player outside world.");
            }

            if (enemy.X < 0 || enemy.X >= GridWidth ||
                enemy.Y < 0 || enemy.Y >= GridHeight)
            {
                throw new ArgumentOutOfRangeException("enemy", enemy, "Enemy outside world.");
            }

            #endregion

            PathNode temp;

            PlayerPosition.X = player.X;
            PlayerPosition.Y = player.Y;

            temp = WorldGrid[PointToIndex(PlayerPosition)];
            temp.IsWalkable = false;
            temp.ParentNode = null;

            temp.G = 0.0;
            temp.H = 0.0;
            temp.F = 0.0;


            EnemyPosition.X = enemy.X;
            EnemyPosition.Y = enemy.Y;

            temp = WorldGrid[PointToIndex(EnemyPosition)];
            temp.IsWalkable = false;
            temp.ParentNode = null;

            PredictEnemyPosition(temp);
        }

        private void PredictEnemyPosition(PathNode enemyNode)
        {
            List<PathNode> posiblePosition = RetrieveWalkableNeighbourhood(enemyNode);

            if (posiblePosition.Count == 0)
            {
                PlayersSeparated();
            }
            else if (posiblePosition.Count == 1)
            {
                ProbableEnemyPosition.X = posiblePosition[0].X;
                ProbableEnemyPosition.Y = posiblePosition[0].Y;
            }
            else if (posiblePosition.Count <= 4)
            {
                //TODO: Implementacja przewidywania nastepnego kroku wroga:
                ProbableEnemyPosition.X = posiblePosition[0].X;
                ProbableEnemyPosition.Y = posiblePosition[0].Y;
            }
            else
            {
                throw new InvalidOperationException("To much possible position");
            }
        }

        #endregion

        #region  Players separation

        public bool Separated { get; private set; }

        public event EventHandler EnemySeparated;

        private void PlayersSeparated()
        {
            if (!Separated)
            {
                Separated = true;

                if (EnemySeparated != null)
                {
                    EnemySeparated(this, null);
                }
            }
        }

        #endregion

        #region Constructor

        public AStarPathfinder(int worldWidth, int wolrdHeight, Point startPlayerPosition, Point startEnemyPosition)
        {
            #region Argument validation

            if (worldWidth <= 0)
            {
                throw new ArgumentOutOfRangeException("width", worldWidth, "Invalid size of the world.");
            }

            if (wolrdHeight <= 0)
            {
                throw new ArgumentOutOfRangeException("height", wolrdHeight, "Invalid size of the world.");
            }

            if (worldWidth == 1 && wolrdHeight == 1)
            {
                throw new ArgumentOutOfRangeException("Invalid size of the world.");
            }

            #endregion

            GridWidth = worldWidth;
            GridHeight = wolrdHeight;
            GridSize = worldWidth * wolrdHeight;

            Separated = false;

            WorldGrid = new PathNode[GridSize];

            for (int y = 0; y < GridHeight; ++y)
            {
                for (int x = 0; x < GridWidth; ++x)
                {
                    WorldGrid[PositionToIndex(x, y)] = new PathNode(x, y);
                }
            }

            UpdatePlayersPosition(startPlayerPosition, startEnemyPosition);

            OpenQueue = new PriorityQueue<double, PathNode>(GridSize);
            ClosedSet = new HashSet<PathNode>();
        }

        #endregion

        #region A* implementation

        public List<Move> FindPathToEnemy()
        {
            if (!Separated)
            {
                return CalculateMovmentsPathFromPlayerToEnemy();
            }

            return new List<Move>(0);
        }

        private PriorityQueue<double, PathNode> OpenQueue;
        private HashSet<PathNode> ClosedSet;

        private List<Move> CalculateMovmentsPathFromPlayerToEnemy()
        {
            PathNode startFrom = WorldGrid[PointToIndex(PlayerPosition)];
            PathNode goalNode = WorldGrid[PointToIndex(ProbableEnemyPosition)];

            OpenQueue.Clear();
            OpenQueue.Insert(startFrom);

            ClosedSet.Clear();


            while (!OpenQueue.IsEmpty)
            {
                PathNode current = OpenQueue.PullHighest();
                if (current.Equals(goalNode))
                {
                    return RetriveMovmentsByPath(current);
                }

                ClosedSet.Add(current);

                List<PathNode> currentSuccessors = RetrieveWalkableNeighbourhood(current);
                for (int i = 0; i < currentSuccessors.Count; ++i)
                {
                    PathNode successor = currentSuccessors[i];

                    if (ClosedSet.Contains(successor))
                    {
                        continue;
                    }

                    if (!OpenQueue.Contains(successor))
                    {
                        successor.HRecalculate(startFrom, goalNode);
                        successor.ParentNode = current;
                        successor.GRecalculate();

                        OpenQueue.Insert(successor);
                    }
                    else
                    {
                        if (successor.G > current.G + 1)
                        {
                            successor.ParentNode = current;
                            successor.GRecalculate();

                            OpenQueue.ItemPriorityChanged(successor);
                        }
                    }
                }
            }

            PlayersSeparated();
            return new List<Move>(0);
        }

        private List<Move> RetriveMovmentsByPath(PathNode finish)
        {
            List<Move> movmentsList = new List<Move>();
            PathNode current = finish;

            while (current.ParentNode != null)
            {
                int dX = current.X - current.ParentNode.X;
                int dY = current.Y - current.ParentNode.Y;

                if (dX == 0 && dY == -1)
                {
                    movmentsList.Add(Move.Up);
                }
                else if (dX == 1 && dY == 0)
                {
                    movmentsList.Add(Move.Right);
                }
                else if (dX == 0 && dY == 1)
                {
                    movmentsList.Add(Move.Down);
                }
                else if (dX == -1 && dY == 0)
                {
                    movmentsList.Add(Move.Left);
                }
                else
                {
                    throw new InvalidOperationException(String.Format("Invalid dX: {0} or dY: {1}.", dX, dY));
                }

                current = current.ParentNode;
            }

            return movmentsList;
        }

        private List<PathNode> RetrieveWalkableNeighbourhood(PathNode forThatOne)
        {
            List<PathNode> neighbourhood = new List<PathNode>(4);
            PathNode neighbour;

            if (forThatOne.Y - 1 >= 0)
            {
                neighbour = WorldGrid[PositionToIndex(forThatOne.X, forThatOne.Y - 1)];
                if (neighbour.IsWalkable)
                {
                    neighbourhood.Add(neighbour);
                }
            }

            if (forThatOne.X + 1 < GridWidth)
            {
                neighbour = WorldGrid[PositionToIndex(forThatOne.X + 1, forThatOne.Y)];
                if (neighbour.IsWalkable)
                {
                    neighbourhood.Add(neighbour);
                }
            }

            if (forThatOne.Y + 1 < GridHeight)
            {
                neighbour = WorldGrid[PositionToIndex(forThatOne.X, forThatOne.Y + 1)];
                if (neighbour.IsWalkable)
                {
                    neighbourhood.Add(neighbour);
                }
            }

            if (forThatOne.X - 1 >= 0)
            {
                neighbour = WorldGrid[PositionToIndex(forThatOne.X - 1, forThatOne.Y)];
                if (neighbour.IsWalkable)
                {
                    neighbourhood.Add(neighbour);
                }
            }

            return neighbourhood;
        }

        #endregion
    }
}
