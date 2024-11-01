

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
        public class Cell
        {
            public bool HasRobot { get; set; } = false;
            public bool HasWall { get; set; } = false;
            public bool HasGoal { get; set; } = false;
        }

        private Size _gridSize;
        private int _cellSize;
        private Cell[,] _grid;

        /// <summary>
        /// The size of the grid, in cells
        /// </summary>
        public Size GridSize { get { return _gridSize; } }
        /// <summary>
        /// The size of each grid cell, in pixels
        /// </summary>
        public int CellSize { get { return _cellSize; } }
        /// <summary>
        /// Gets the entire grid
        /// </summary>
        public Cell[,] GetGrid { get { return _grid; } }

        /// <summary>
        /// Manages a grid of the chosen size
        /// </summary>
        /// <param name="gridSize">The size of the grid in cells</param>
        /// <param name="cellSize">The size of each cell in pixels (for display)</param>
        public Grid(Size gridSize, int cellSize)
        {
            _gridSize = gridSize;
            _cellSize = cellSize;

            //Initialize the grid
            _grid = new Cell[_gridSize.Width, _gridSize.Height];

            for (var x = 0; x < _gridSize.Width; x++)
            {
                for (var y = 0; y < _gridSize.Height; y++)
                {
                    _grid[x, y] = new Cell();
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
            return PosInsideGrid(pos) && !_grid[pos.X, pos.Y].HasWall;
        }

        /// <summary>
        /// A method for checking if the given point is contained within the grid
        /// </summary>
        /// <param name="pos">The grid position to check</param>
        /// <returns>Whether the given position is contained within the grid</returns>
        public bool PosInsideGrid(Point pos)
        {
            var posInsideX = pos.X >= 0 && pos.X < _gridSize.Width;
            var posInsideY = pos.Y >= 0 && pos.Y < _gridSize.Height;
            return posInsideX && posInsideY;
        }

        /// <summary>
        /// Converts a given grid position to pixel coordinates
        /// </summary>
        /// <param name="pos">The grid position to convert</param>
        /// <returns>The relative world positon of the centre of the given cell</returns>
        public Point CellToWorld(Point pos)
        {
            var cellPosX = pos.X * _cellSize + (_cellSize / 2);
            var cellPosY = pos.Y * _cellSize + (_cellSize / 2);
            return new Point(cellPosX, cellPosY);
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
                case Content.Robot: _grid[pos.X, pos.Y].HasRobot = true; break;
                case Content.Wall: _grid[pos.X, pos.Y].HasWall = true; break;
                case Content.Goal: _grid[pos.X, pos.Y].HasGoal = true; break;
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
                case Content.Robot: _grid[pos.X, pos.Y].HasRobot = false; break;
                case Content.Wall: _grid[pos.X, pos.Y].HasWall = false; break;
                case Content.Goal: _grid[pos.X, pos.Y].HasGoal = false; break;
            }
        }

        /// <summary>
        /// Clears the given cell of all content
        /// </summary>
        /// <param name="pos">The grid position at which to clear all content</param>
        public void ClearCell(Point pos)
        {
            if (!PosInsideGrid(pos)) return;

            _grid[pos.X, pos.Y].HasRobot = false;
            _grid[pos.X, pos.Y].HasWall = false;
            _grid[pos.X, pos.Y].HasGoal = false;
        }

        /// <summary>
        /// Clears all cells in the grid of all content
        /// </summary>
        public void ClearAllCells()
        {
            Point currentPoint = new Point();

            for (currentPoint.X = 0; currentPoint.X < _grid.GetLength(0); currentPoint.X++)
            {
                for (currentPoint.Y = 0; currentPoint.Y < _grid.GetLength(1); currentPoint.Y++)
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
            for (var x = 0; x < _gridSize.Width; x++)
            {
                for (var y = 0; y < _gridSize.Height; y++)
                {                
                    if (_grid[x, y].HasWall) DrawContent(new Point(x, y), graphics, wallBrush);
                    if (_grid[x, y].HasGoal) DrawContent(new Point(x, y), graphics, goalBrush);
                }
            }

            //Draw Grid
            int marginSize = Constants.MARGIN_SIZE;
            Point horizontalStart, horizontalEnd, verticalStart, verticalEnd;

            //Vertical Lines
            for (var x = 0; x <= _gridSize.Width; x++)
            {
                verticalStart = new Point(_cellSize * x, 0);
                verticalEnd = new Point(_cellSize * x, _cellSize * _gridSize.Height);

                verticalStart.Offset(marginSize, marginSize);
                verticalEnd.Offset(marginSize, marginSize);

                graphics.DrawLine(gridPen, verticalStart, verticalEnd);
            }

            //Horizontal Lines
            for (var y = 0; y <= _gridSize.Height; y++)
            {
                horizontalStart = new Point(0, _cellSize * y);
                horizontalEnd = new Point(_cellSize * _gridSize.Width, _cellSize * y);

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
            var objSize = new Size(_cellSize, _cellSize);
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
