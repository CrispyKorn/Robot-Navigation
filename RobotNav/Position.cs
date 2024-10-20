using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class Position : IEquatable<Position>
    {
        /// <summary>
        /// The position in grid coordinates
        /// </summary>
        public Point position { get; private set; }

        /// <summary>
        /// Represents a cell position on the grid
        /// </summary>
        /// <param name="_position">The grid position represented by this object</param>
        public Position(Point _position)
        {
            position = _position;
        }

        //Equality Checkers
        public override bool Equals(object? obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position? other)
        {
            return other != null && position == other.position;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(position);
        }
    }
}
