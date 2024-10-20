using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RobotNav
{
    internal class Robot
    {
        Color color;
        Rectangle body;
        Point pos;
        Grid grid;

        /// <summary>
        /// The grid position of the robot
        /// </summary>
        public Point Pos { get { return pos; } }
        /// <summary>
        /// The color of the robot
        /// </summary>
        public Color Color { get { return color; } }

        /// <summary>
        /// Represents and manages a robot for use in a grid
        /// </summary>
        /// <param name="_color">The colour of the robot</param>
        /// <param name="startPos">The starting position of the robot</param>
        /// <param name="_grid">The grid on which the robot will exist</param>
        public Robot(Color _color, Point startPos, Grid _grid)
        {
            color = _color;
            pos = startPos;
            grid = _grid;
            int robotSize = grid.CellSize * 2 / 3;
            body = new Rectangle(pos, new Size(robotSize, robotSize));

            //Position the robot correctly in the grid display
            body.Location = GetNewRobotLocation();
            //Place the robot into the grid
            grid.PlaceInCell(Grid.Content.Robot, pos);
        }

        /// <summary>
        /// Moves the robot in grid coordinates relative to its current position
        /// </summary>
        /// <param name="x">The amount to move on the X-Axis</param>
        /// <param name="y">The amount to move on the Y-Axis</param>
        public void Move(int x, int y)
        {
            //Validate the given movement as a walkable position
            if (!grid.MoveablePos(new Point(pos.X + x, pos.Y + y))) return;

            //Move the robot
            grid.RemoveFromCell(Grid.Content.Robot, pos);
            pos.X += x;
            pos.Y += y;
            grid.PlaceInCell(Grid.Content.Robot, pos);

            //Update the body position
            body.Location = GetNewRobotLocation();
        }

        /// <summary>
        /// Moves the robot directly to the specified grid position
        /// </summary>
        /// <param name="movePos">The grid position to move the robot to</param>
        public void MoveTo(Point movePos)
        {
            //Validate the given position as a walkable position
            if (!grid.MoveablePos(movePos)) return;

            //Move the robot
            grid.RemoveFromCell(Grid.Content.Robot, pos);
            pos = movePos;
            grid.PlaceInCell(Grid.Content.Robot, pos);

            //Update the body position
            body.Location = GetNewRobotLocation();
        }

        /// <summary>
        /// Offsets the robot's body position to fit properly into the grid display
        /// </summary>
        /// <returns>Pixel coordinates at which to draw the body</returns>
        private Point GetNewRobotLocation()
        {
            Point newLocation = grid.CellToWorld(pos);

            newLocation.Offset(new Point(-body.Height / 2 + 1, -body.Height / 2 + 1));
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
            graphics.FillRectangle(brush, body);
        }
    }
}
