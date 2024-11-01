

namespace RobotNav
{
    internal class Robot
    {
        private Color _color;
        private Rectangle _body;
        private Point _pos;
        private Grid _grid;

        /// <summary>
        /// The grid position of the robot
        /// </summary>
        public Point Pos { get { return _pos; } }
        /// <summary>
        /// The color of the robot
        /// </summary>
        public Color Color { get { return _color; } }

        /// <summary>
        /// Represents and manages a robot for use in a grid
        /// </summary>
        /// <param name="color">The colour of the robot</param>
        /// <param name="startPos">The starting position of the robot</param>
        /// <param name="grid">The grid on which the robot will exist</param>
        public Robot(Color color, Point startPos, Grid grid)
        {
            _color = color;
            _pos = startPos;
            _grid = grid;
            int robotSize = _grid.CellSize * 2 / 3;
            _body = new Rectangle(_pos, new Size(robotSize, robotSize));

            _body.Location = GetNewRobotLocation(); // Position the robot correctly in the grid display
            _grid.PlaceInCell(Grid.Content.Robot, _pos); // Place the robot into the grid
        }

        /// <summary>
        /// Moves the robot in grid coordinates relative to its current position
        /// </summary>
        /// <param name="moveX">The amount to move on the X-Axis</param>
        /// <param name="moveY">The amount to move on the Y-Axis</param>
        public void Move(int moveX, int moveY)
        {
            // Validate the given movement as a walkable position
            if (!_grid.MoveablePos(new Point(_pos.X + moveX, _pos.Y + moveY))) return;

            // Move the robot
            _grid.RemoveFromCell(Grid.Content.Robot, _pos);
            _pos.X += moveX;
            _pos.Y += moveY;
            _grid.PlaceInCell(Grid.Content.Robot, _pos);

            // Update the body position
            _body.Location = GetNewRobotLocation();
        }

        /// <summary>
        /// Moves the robot directly to the specified grid position
        /// </summary>
        /// <param name="movePos">The grid position to move the robot to</param>
        public void MoveTo(Point movePos)
        {
            // Validate the given position as a walkable position
            if (!_grid.MoveablePos(movePos)) return;

            // Move the robot
            _grid.RemoveFromCell(Grid.Content.Robot, _pos);
            _pos = movePos;
            _grid.PlaceInCell(Grid.Content.Robot, _pos);

            // Update the body position
            _body.Location = GetNewRobotLocation();
        }

        /// <summary>
        /// Offsets the robot's body position to fit properly into the grid display
        /// </summary>
        /// <returns>Pixel coordinates at which to draw the body</returns>
        private Point GetNewRobotLocation()
        {
            Point newLocation = _grid.CellToWorld(_pos);

            newLocation.Offset(new Point(-_body.Height / 2 + 1, -_body.Height / 2 + 1));
            newLocation.Offset(Constants.MARGIN_SIZE, Constants.MARGIN_SIZE);

            return newLocation;
        }

        /// <summary>
        /// Draws the robot to the screen
        /// </summary>
        /// <param name="graphics">The graphics to draw to</param>
        /// <param name="brush">The brush to draw with</param>
        public void DrawRobot(Graphics graphics, Brush brush)
        {
            graphics.FillRectangle(brush, _body);
        }
    }
}
