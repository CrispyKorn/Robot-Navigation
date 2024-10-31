using System.Text.RegularExpressions;

namespace RobotNav
{
    public partial class MainWindow : Form
    {
        Robot robot;
        Point startPos;
        Grid grid;
        List<Point> goalPositions;
        Graphics graphics;
        Pen gridPen;
        SolidBrush robotBrush, wallBrush, goalBrush;

        bool changeMade = true;

        //File Handling
        string textFile = Environment.CurrentDirectory + "\\RobotNav.txt";
        string[] text;
        string[] currentLineValues;

        //Playback
        LinkedList<State<Position>> solution;
        System.Windows.Forms.Timer stepTimer;

        public MainWindow()
        {
            InitializeComponent();

            //Timer
            stepTimer = new System.Windows.Forms.Timer();
            stepTimer.Interval = 500;
            stepTimer.Tick += StepTimer_Tick;

            //File Input
            text = File.ReadAllLines(textFile);

            //Grid
            currentLineValues = text[0].Split(',');
            
            int gridSizeY = int.Parse((Regex.Match(currentLineValues[0], @"\d+").Value));
            int gridSizeX = int.Parse((Regex.Match(currentLineValues[1], @"\d+").Value));
            Size gridSize = new Size(gridSizeX, gridSizeY);

            grid = new Grid(gridSize, 25);

            //Vacuum
            currentLineValues = text[1].Split(',');

            int robotPosX = int.Parse((Regex.Match(currentLineValues[0], @"\d+").Value));
            int robotPosY = int.Parse((Regex.Match(currentLineValues[1], @"\d+").Value));
            Point robotPos = new Point(robotPosX, robotPosY);

            robot = new Robot(Color.Aquamarine, robotPos, grid);
            startPos = robotPos;

            //Goals
            goalPositions = new List<Point>();
            currentLineValues = text[2].Split('|');

            foreach (string currentGoalValues in currentLineValues)
            {
                string[] goalValues = currentGoalValues.Split(',');

                int goalPosX = int.Parse((Regex.Match(goalValues[0], @"\d+").Value));
                int goalPosY = int.Parse((Regex.Match(goalValues[1], @"\d+").Value));
                Point goalPos = new Point(goalPosX, goalPosY);

                goalPositions.Add(goalPos);
                grid.PlaceInCell(Grid.Content.Goal, goalPos);
            }

            //Walls
            for (int i = 3; i < text.Length; i++)
            {
                currentLineValues = text[i].Split(',');
                Point wallPos = new Point(int.Parse((Regex.Match(currentLineValues[0], @"\d+").Value)), int.Parse((Regex.Match(currentLineValues[1], @"\d+").Value)));
                Size wallSize = new Size(int.Parse((Regex.Match(currentLineValues[2], @"\d+").Value)), int.Parse((Regex.Match(currentLineValues[3], @"\d+").Value)));

                for (int xOffset = 0; xOffset < wallSize.Width; xOffset++)
                {
                    for (int yOffset = 0; yOffset < wallSize.Height; yOffset++)
                    {
                        grid.PlaceInCell(Grid.Content.Wall, new Point(wallPos.X + xOffset, wallPos.Y + yOffset));
                    }
                }
            }

            //Initialize
            graphics = CreateGraphics();
            robotBrush = new SolidBrush(robot.Color);
            wallBrush = new SolidBrush(Color.Black);
            goalBrush = new SolidBrush(Color.ForestGreen);
            gridPen = new Pen(Color.Black);
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
            if (changeMade)
            {
                graphics.Clear(Color.DimGray);

                grid.DrawGrid(graphics, gridPen, wallBrush, goalBrush);
                robot.DrawRobot(graphics, robotBrush);

                changeMade = false;
            }
        }

        private void StepTimer_Tick(object? sender, EventArgs e)
        {
            //Stop the timer when the end of the solution is reached
            if (solution == null || solution.Count == 0)
            {
                stepTimer.Stop();
                return;
            }

            //Move vacuum to next position in the solution
            Point movePos = solution.First.Value.data.position;
            solution.RemoveFirst();
            int moveX = movePos.X - robot.Pos.X;
            int moveY = movePos.Y - robot.Pos.Y;

            robot.Move(moveX, moveY);

            //Update the scene
            changeMade = true;
            DrawScene();
        }

        /// <summary>
        /// Commences the pathfinding search, printing the solution and beginning the timer to step through the solution
        /// </summary>
        private void Search()
        {
            //Initialize the desired scenario
            SingleGoalScenario singleGoalScenario = new SingleGoalScenario(grid, goalPositions[0]);

            //Initialize the desired pathfinding algorithm
            Pathfinder<Position>? routeFinder = null;
            if (rad_BFS.Checked) routeFinder = new BFS<Position>();
            if (rad_AStar.Checked) routeFinder = new AStar<Position>();
            if (rad_DFS.Checked) routeFinder = new DFS<Position>();
            if (rad_GBFS.Checked) routeFinder = new GBFS<Position>();
            if (rad_Djikstra.Checked) routeFinder = new Dijkstra<Position>();
            if (rad_IDFS.Checked) routeFinder = new IDFS<Position>();

            //Set the desired goal to be reached
            int goalNum = -1;
            if (rad_Goal1.Checked) goalNum = 0;
            if (rad_Goal2.Checked) goalNum = 1;

            //Set the initial state and goal state
            State<Position> initialState = new State<Position>(new Position(new Point(robot.Pos.X, robot.Pos.Y)), null, "Initial State", grid.GetHCost(robot.Pos, goalPositions[0]), 0f);
            State<Position> goalState = new State<Position>(new Position(new Point(goalPositions[goalNum].X, goalPositions[goalNum].Y)), null, "Goal State");

            //Commence the search
            if (routeFinder != null) solution = routeFinder.Search(initialState, goalState, singleGoalScenario, tbx_Output);

            //Once the solution has been found, start the timer for stepping through the solution
            stepTimer.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            //Reset the program
            stepTimer.Stop();
            solution.Clear();
            robot.MoveTo(startPos);
            tbx_Output.Text = "";
            changeMade = true;
            DrawScene();
        }
    }
}