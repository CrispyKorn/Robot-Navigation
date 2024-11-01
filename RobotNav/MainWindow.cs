using System.Text.RegularExpressions;

namespace RobotNav
{
    public partial class MainWindow : Form
    {
        private Robot _robot;
        private Point _startPos;
        private Grid _grid;
        private List<Point> _goalPositions = new();
        private Graphics _graphics;
        private Pen _gridPen;
        private SolidBrush _robotBrush, _wallBrush, _goalBrush;

        private bool _changeMade = true;

        //File Handling
        private string _textFile;
        private string[] _text;
        private string[] _currentLineValues;

        //Playback
        private LinkedList<State<GridPosition>> _solution = new();
        private System.Windows.Forms.Timer _stepTimer;

        public MainWindow()
        {
            InitializeComponent();

            // Setup file
            _textFile = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\RobotNav.txt"));
            _text = File.ReadAllLines(_textFile);

            // Timer
            _stepTimer = new System.Windows.Forms.Timer();
            _stepTimer.Interval = 500;
            _stepTimer.Tick += StepTimer_Tick;

            // Grid
            _currentLineValues = _text[0].Split(',');
            
            var gridSizeY = int.Parse(Regex.Match(_currentLineValues[0], @"\d+").Value);
            var gridSizeX = int.Parse(Regex.Match(_currentLineValues[1], @"\d+").Value);
            var gridSize = new Size(gridSizeX, gridSizeY);

            _grid = new Grid(gridSize, 25);

            // Vacuum
            _currentLineValues = _text[1].Split(',');

            var robotPosX = int.Parse(Regex.Match(_currentLineValues[0], @"\d+").Value);
            var robotPosY = int.Parse(Regex.Match(_currentLineValues[1], @"\d+").Value);
            var robotPos = new Point(robotPosX, robotPosY);

            _robot = new Robot(Color.Aquamarine, robotPos, _grid);
            _startPos = robotPos;

            // Goals
            _currentLineValues = _text[2].Split('|');

            foreach (string currentGoalValues in _currentLineValues)
            {
                string[] goalValues = currentGoalValues.Split(',');

                var goalPosX = int.Parse(Regex.Match(goalValues[0], @"\d+").Value);
                var goalPosY = int.Parse(Regex.Match(goalValues[1], @"\d+").Value);
                var goalPos = new Point(goalPosX, goalPosY);

                _goalPositions.Add(goalPos);
                _grid.PlaceInCell(Grid.Content.Goal, goalPos);
            }

            // Walls
            for (int i = 3; i < _text.Length; i++)
            {
                _currentLineValues = _text[i].Split(',');
                var wallPosX = int.Parse(Regex.Match(_currentLineValues[0], @"\d+").Value);
                var wallPosY = int.Parse(Regex.Match(_currentLineValues[1], @"\d+").Value);
                var wallWidth = int.Parse(Regex.Match(_currentLineValues[2], @"\d+").Value);
                var wallHeight = int.Parse(Regex.Match(_currentLineValues[3], @"\d+").Value);
                var wallPos = new Point(wallPosX, wallPosY);
                var wallSize = new Size(wallWidth, wallHeight);

                for (int xOffset = 0; xOffset < wallSize.Width; xOffset++)
                {
                    for (int yOffset = 0; yOffset < wallSize.Height; yOffset++)
                    {
                        _grid.PlaceInCell(Grid.Content.Wall, new Point(wallPos.X + xOffset, wallPos.Y + yOffset));
                    }
                }
            }

            // Initialize
            _graphics = CreateGraphics();
            _robotBrush = new SolidBrush(_robot.Color);
            _wallBrush = new SolidBrush(Color.Black);
            _goalBrush = new SolidBrush(Color.ForestGreen);
            _gridPen = new Pen(Color.Black);
        }

        private void MainWindow_Paint(object sender, EventArgs e)
        {
            DrawScene();
        }

        /// <summary>
        /// Draws all necessary content to the screen for updating the scene
        /// </summary>
        private void DrawScene()
        {
            if (_changeMade)
            {
                _graphics.Clear(Color.DimGray);

                _grid.DrawGrid(_graphics, _gridPen, _wallBrush, _goalBrush);
                _robot.DrawRobot(_graphics, _robotBrush);

                _changeMade = false;
            }
        }

        private void StepTimer_Tick(object? sender, EventArgs e)
        {
            // Stop the timer when the end of the solution is reached
            if (_solution == null || _solution.Count == 0)
            {
                _stepTimer.Stop();
                return;
            }

            // Move vacuum to next position in the solution
            Point movePos = _solution.First.Value.Data.Position;
            _solution.RemoveFirst();
            int moveX = movePos.X - _robot.Pos.X;
            int moveY = movePos.Y - _robot.Pos.Y;

            _robot.Move(moveX, moveY);

            //Update the scene
            _changeMade = true;
            DrawScene();
        }

        /// <summary>
        /// Commences the pathfinding search, printing the solution and beginning the timer to step through the solution
        /// </summary>
        private void Search()
        {
            // Initialize the desired scenario
            SingleGoalScenario singleGoalScenario = new SingleGoalScenario(_grid, _goalPositions[0]);

            // Initialize the desired pathfinding algorithm
            Pathfinder<GridPosition>? routeFinder = null;
            if (rad_BFS.Checked) routeFinder = new BFS<GridPosition>();
            if (rad_AStar.Checked) routeFinder = new AStar<GridPosition>();
            if (rad_DFS.Checked) routeFinder = new DFS<GridPosition>();
            if (rad_GBFS.Checked) routeFinder = new GBFS<GridPosition>();
            if (rad_Djikstra.Checked) routeFinder = new Dijkstra<GridPosition>();
            if (rad_IDFS.Checked) routeFinder = new IDFS<GridPosition>();

            // Set the desired goal to be reached
            var goalNum = -1;
            if (rad_Goal1.Checked) goalNum = 0;
            if (rad_Goal2.Checked) goalNum = 1;

            // Set the initial state and goal state
            var initialState = new State<GridPosition>(new GridPosition(new Point(_robot.Pos.X, _robot.Pos.Y)), null, "Initial State", _grid.GetHCost(_robot.Pos, _goalPositions[0]), 0f);
            var goalState = new State<GridPosition>(new GridPosition(new Point(_goalPositions[goalNum].X, _goalPositions[goalNum].Y)), null, "Goal State");

            // Commence the search
            if (routeFinder != null) _solution = routeFinder.Search(initialState, goalState, singleGoalScenario, tbx_Output);

            // Once the solution has been found, start the timer for stepping through the solution
            _stepTimer.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            // Reset the program
            _stepTimer.Stop();
            _solution.Clear();
            _robot.MoveTo(_startPos);
            tbx_Output.Text = "";
            _changeMade = true;
            DrawScene();
        }
    }
}