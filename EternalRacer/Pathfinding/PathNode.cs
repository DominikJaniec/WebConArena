using EternalRacer.PriorityQueue;

namespace EternalRacer.Pathfinding
{
    internal class PathNode : IPriorityItem<double, PathNode>
    {
        #region Public fields

        // HeuristicBreakingTiesFactor := 1 / MAX_EXPECTED_PATH_LENGTH
        public static double HeuristicBreakingTiesFactor = 1.0 / 1500;

        public PathNode ParentNode;

        public readonly int X;
        public readonly int Y;

        public bool IsWalkable;

        #endregion

        #region F - Current cost

        public double F;

        #endregion
        #region G - Exact cost from Start

        public void GRecalculate()
        {
            G = ParentNode.G + 1;
            F = G + H;
        }

        public double G;

        #endregion
        #region H - Estimated cost to Goal

        public void HRecalculate(PathNode start, PathNode goal)
        {
            #region Manhattan heuristic

            int dX = X - goal.X;
            dX = (dX < 0) ? (-1) * dX : dX;

            int dY = Y - goal.Y;
            dY = (dY < 0) ? (-1) * dY : dY;

            H = (dX + dY);

            #endregion
            #region Breaking ties - Line: start-goal

            int dX1 = X - goal.X;
            int dY1 = Y - goal.Y;
            int dX2 = start.X - goal.X;
            int dY2 = start.Y - goal.Y;

            int cross = dX1 * dY2 - dX2 * dY1;
            cross = (cross < 0) ? (-1) * cross : cross;

            H += cross * 0.001;

            #endregion

            F = G + H;
        }

        public double H;

        #endregion

        #region Constructor

        public PathNode(int x, int y)
        {
            X = x;
            Y = y;

            HashCode = X << 16 | Y;

            ParentNode = null;
            IsWalkable = true;

            H = 0.0;
            G = 0.0;
            F = 0.0;
        }

        #endregion

        #region Overrides for HasSet

        private readonly int HashCode;
        public override int GetHashCode()
        {
            return HashCode;
        }

        public override bool Equals(object obj)
        {
            if (HashCode == obj.GetHashCode())
            {
                return obj is PathNode;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IPriorityItem Implementation

        public double PriorityKey { get { return F; } }
        public bool IsMoreImportantThan(PathNode thatOne)
        {
            return F <= thatOne.F;
        }

        #endregion
    }
}
