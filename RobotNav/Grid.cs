using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class Grid
    {
        /// <summary>
        /// All possible content that can be in a grid cell
        /// </summary>
        public enum Content
        {
            Nothing,
            Robot,
            Wall,
            Goal
        }

        /// <summary>
        /// Describes a grid cell
        /// </summary>
        public struct Cell
        {
            public bool hasRobot;
            public bool hasWall;
            public bool hasGoal;
        }

        Size gridSize;
        int cellSize;
        Cell[,] grid;

        /// <summary>
        /// The size of the grid, in cells
        /// </summary>
        public Size GridSize { get { return gridSize; } }
        /// <summary>
        /// The size of each grid cell, in pixels
        /// </summary>
        public int CellSize { get { return cellSize; } }
        /// <summary>
        /// Gets the entire grid
        /// </summary>
        public Cell[,] GetGrid { get { return grid; } }

        /// <summary>
        /// Manages a grid of the chosen size
        /// </summary>
        /// <param name="_gridSize">The size of the grid in cells</param>
        /// <param name="_cellSize">The size of each cell in pixels (for display)</param>
        public Grid(Size _gridSize, int _cellSize)
        {
            gridSize = _gridSize;
            cellSize = _cellSize;

            //Initialize the grid
            grid = new Cell[gridSize.Width, gridSize.Height];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y].hasRobot = false;
                    grid[x, y].hasWall = false;
                    grid[x, y].hasGoal = false;
                }
            }
        }

        /// <summary>
        /// A method for checking if the given point is a moveable position for the robot
        /// </summary>
        /// <param name="pos">The grid position to check</param>
        /// <returns>Whether the given position is moveable</returns>
        public bool MoveablePos(Point pos)
        {
            return PosInsideGrid(pos) && !grid[pos.X, pos.Y].hasWall;
        }

        /// <summary>
        /// A method for checking if the given point is contained within the grid
        /// </summary>
        /// <param name="pos">The grid position to check</param>
        /// <returns>Whether the given position is contained within the grid</returns>
        public bool PosInsideGrid(Point pos)
        {
            return pos.X >= 0 && pos.X < gridSize.Width && pos.Y >= 0 && pos.Y < gridSize.Height;
        }

        /// <summary>
        /// Converts a given grid position to pixel coordinates
        /// </summary>
        /// <param name="pos">The grid position to convert</param>
        /// <returns>The relative world positon of the given cell</returns>
        public Point CellToWorld(Point pos)
        {
            return new Point(pos.X * cellSize + (cellSize / 2), pos.Y * cellSize + (cellSize / 2));
        }

        /// <summary>
        /// Places content into a cell
        /// </summary>
        /// <param name="content">The content to add</param>
        /// <param name="pos">The grid position at which to add content</param>
        public void PlaceInCell(Content content, Point pos)
        {
            //Make sure the given cell is inside the grid
            if (!PosInsideGrid(pos)) return;

            //Add the corresponding content
            switch (content)
            {
                case Content.Robot: grid[pos.X, pos.Y].hasRobot = true; break;
                case Content.Wall: grid[pos.X, pos.Y].hasWall = true; break;
                case Content.Goal: grid[pos.X, pos.Y].hasGoal = true; break;
            }
        }

        /// <summary>
        /// Removes content from a cell
        /// </summary>
        /// <param name="content">The content to remove</param>
        /// <param name="pos">The grid position at which to remove content</param>
        public void RemoveFromCell(Content content, Point pos)
        {
            //Make sure the given cell is inside the grid
            if (!PosInsideGrid(pos)) return;

            //Remove the corresponding content
            switch (content)
            {
                case Content.Robot: grid[pos.X, pos.Y].hasRobot = false; break;
                case Content.Wall: grid[pos.X, pos.Y].hasWall = false; break;
                case Content.Goal: grid[pos.X, pos.Y].hasGoal = false; break;
            }
        }

        /// <summary>
        /// Clears the given cell of all content
        /// </summary>
        /// <param name="pos">The grid position at which to clear all content</param>
        public void ClearCell(Point pos)
        {
            if (!PosInsideGrid(pos)) return;

            grid[pos.X, pos.Y].hasRobot = false;
            grid[pos.X, pos.Y].hasWall = false;
            grid[pos.X, pos.Y].hasGoal = false;
        }

        /// <summary>
        /// Clears all cells in the grid of all content
        /// </summary>
        public void ClearAllCells()
        {
            Point currentPoint = new Point();

            for (currentPoint.X = 0; currentPoint.X < grid.GetLength(0); currentPoint.X++)
            {
                for (currentPoint.Y = 0; currentPoint.Y < grid.GetLength(1); currentPoint.Y++)
                {
                    ClearCell(currentPoint);
                }
            }
        }

        /// <summary>
        /// Draws the grid to the screen
        /// </summary>
        /// <param name="graphics">The graphics with which to draw</param>
        /// <param name="gridPen">The pen for drawing the grid lines</param>
        /// <param name="wallBrush">The brush for drawing walls</param>
        /// <param name="goalBrush">The brush for drawing the goals</param>
        public void DrawGrid(Graphics graphics, Pen gridPen, Brush wallBrush, Brush goalBrush)
        {
            //Draw Content
            for (int x = 0; x < gridSize.Width; x++)
            {
                for (int y = 0; y < gridSize.Height; y++)
                {                
                    if (grid[x, y].hasWall) DrawContent(new Point(x, y), graphics, wallBrush);
                    if (grid[x, y].hasGoal) DrawContent(new Point(x, y), graphics, goalBrush);
                }
            }

            //Draw Grid
            int marginSize = Constants.MARGIN_SIZE;
            Point horizontalStart, horizontalEnd, verticalStart, verticalEnd;

            //Vertical Lines
            for (int x = 0; x <= gridSize.Width; x++)
            {
                verticalStart = new Point(cellSize * x, 0);
                verticalEnd = new Point(cellSize * x, cellSize * gridSize.Height);

                verticalStart.Offset(marginSize, marginSize);
                verticalEnd.Offset(marginSize, marginSize);

                graphics.DrawLine(gridPen, verticalStart, verticalEnd);
            }

            //Horizontal Lines
            for (int y = 0; y <= gridSize.Height; y++)
            {
                horizontalStart = new Point(0, cellSize * y);
                horizontalEnd = new Point(cellSize * gridSize.Width, cellSize * y);

                horizontalStart.Offset(marginSize, marginSize);
                horizontalEnd.Offset(marginSize, marginSize);

                graphics.DrawLine(gridPen, horizontalStart, horizontalEnd);
            }
        }

        /// <summary>
        /// Colour a cell with the given brush
        /// </summary>
        /// <param name="pos">The grid position to draw at</param>
        /// <param name="graphics">The graphics to draw to</param>
        /// <param name="brush">The brush to draw with</param>
        private void DrawContent(Point pos, Graphics graphics, Brush brush)
        {
            //Initialize the required components
            Size objSize = new Size(cellSize, cellSize);
            Point currentPos = CellToWorld(pos);
            int marginSize = Constants.MARGIN_SIZE;

            //Offset the current drawing position to fit into the cell properly
            currentPos.Offset(new Point(-objSize.Width / 2, -objSize.Height / 2));
            currentPos.Offset(new Point(marginSize, marginSize));

            //Draw!
            graphics.FillRectangle(brush, new Rectangle(currentPos, objSize));
        }

        /// <summary>
        /// Get the estimated cost to reach the goal cell from the given cell
        /// </summary>
        /// <param name="pos">The position from which to check the distance to the goal</param>
        /// <param name="goalPos">The goals grid position</param>
        /// <returns></returns>
        public float GetHCost(Point pos, Point goalPos)
        {
            //Pythagoras!
            int distX = goalPos.X - pos.X;
            int distY = goalPos.Y - pos.Y;
            return MathF.Sqrt(distX * distX + distY * distY);
        }
    }
}
