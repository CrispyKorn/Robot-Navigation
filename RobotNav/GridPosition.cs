

namespace RobotNav
{
    internal class GridPosition : IEquatable<GridPosition>
    {
        /// <summary>
        /// The position in grid coordinates
        /// </summary>
        public Point Position { get; private set; }

        /// <summary>
        /// Represents a cell position on the grid
        /// </summary>
        /// <param name="position">The grid position represented by this object</param>
        public GridPosition(Point position)
        {
            Position = position;
        }

        //Equality Checkers
        public override bool Equals(object? obj)
        {
            return Equals(obj as GridPosition);
        }

        public bool Equals(GridPosition? other)
        {
            return other != null && Position == other.Position;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position);
        }
    }
}
