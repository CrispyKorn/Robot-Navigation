using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class SingleGoalScenario: Scenario<GridPosition>
    {
        Grid grid;
        Point goalPos;

        /// <summary>
        /// Establishes the critical components required in a pathfinder search
        /// </summary>
        /// <param name="_grid">The grid on which the search is to be performed</param>
        /// <param name="_goalPos">The target position on the grid to be reached</param>
        public SingleGoalScenario(Grid _grid, Point _goalPos)
        {
            grid = _grid;
            goalPos = _goalPos;
        }

        /// <summary>
        /// Checks if the given state is equal to the goal state
        /// </summary>
        /// <param name="stateToCheck">The state to be checked against the goal state</param>
        /// <param name="goalState">The target/goal state</param>
        /// <returns>Whether or not the given state matches the given goal state</returns>
        public override bool IsGoalState(State<GridPosition> stateToCheck, State<GridPosition> goalState)
        {
            return stateToCheck.Data.Position == goalState.Data.Position;
        }

        /// <summary>
        /// Finds and returns all possible moves that can be made from a given state
        /// </summary>
        /// <param name="state">The state from which to get all possible moves</param>
        /// <returns>A list of states corresponding to all direct possible moves in order of (up, down, left, right)</returns>
        public override List<State<GridPosition>> GetPossibleMoves(State<GridPosition> state)
        {
            List<State<GridPosition>> moves = new List<State<GridPosition>>();
            Point startingPos = state.Data.Position;
            Point up = new Point(startingPos.X, startingPos.Y - 1);
            Point left = new Point(startingPos.X - 1, startingPos.Y);
            Point down = new Point(startingPos.X, startingPos.Y + 1);
            Point right = new Point(startingPos.X + 1, startingPos.Y);

            if (grid.MoveablePos(up)) AddNewState(ref moves, up, "up", state);
            if (grid.MoveablePos(left)) AddNewState(ref moves, left, "left", state);
            if (grid.MoveablePos(down)) AddNewState(ref moves, down, "down", state);
            if (grid.MoveablePos(right)) AddNewState(ref moves, right, "right", state);

            return moves;
        }

        /// <summary>
        /// Initializes and adds a new state to the given moves list
        /// </summary>
        /// <param name="moves">A reference to the list of moves the state should be added to</param>
        /// <param name="posToAdd">The grid-based position to assign to the state</param>
        /// <param name="name">The name of the new state</param>
        /// <param name="initialState">The parent state from which this state is expanded from</param>
        private void AddNewState(ref List<State<GridPosition>> moves, Point posToAdd, string name, State<GridPosition> initialState)
        {
            moves.Add(new State<GridPosition>(
                new GridPosition(posToAdd), 
                initialState,
                name, 
                grid.GetHCost(posToAdd, goalPos), 
                initialState.GCost + 1f));
        }
    }
}
