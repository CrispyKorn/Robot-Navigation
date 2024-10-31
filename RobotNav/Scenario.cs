using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal abstract class Scenario<T>
    {
        /// <summary>
        /// Checks if the given state is equal to the goal state
        /// </summary>
        /// <param name="stateToCheck">The state to be checked against the goal state</param>
        /// <param name="goalState">The target/goal state</param>
        /// <returns>Whether or not the given state matches the given goal state</returns>
        public abstract bool IsGoalState(State<T> stateToCheck, State<T> goalState);

        /// <summary>
        /// Finds and returns all possible moves that can be made from a given state
        /// </summary>
        /// <param name="state">The state from which to get all possible moves</param>
        /// <returns>A list of states corresponding to all direct possible moves in order of (up, down, left, right)</returns>
        public abstract List<State<T>> GetPossibleMoves(State<T> state);

        /// <summary>
        /// Prints the final path from the goal state to the given textbox
        /// </summary>
        /// <param name="finalState">The goal state reached at the end of the search</param>
        /// <param name="discovered">The number of nodes discovered</param>
        /// <param name="searched">The number of nodes searched / expanded</param>
        /// <param name="txtbox">The textbox in which to display the solution</param>
        public void PrintSolution(State<T> finalState, int discovered, int searched, TextBox txtbox, bool foundGoal)
        {
            //Print it out!
            txtbox.Text = "Nodes discovered: " + discovered + " | Nodes searched: " + searched + Environment.NewLine;
            
            if (!foundGoal)
            {
                txtbox.Text += "No Solution Found.";
                return;
            }
                
            State<T> currentState = finalState;
            Stack<string> solution = new Stack<string>();

            //Traverse up the chain of states in reverse
            while (currentState.parent != null)
            {
                //Add each state to the start of the solution (to keep them in order when printed)
                solution.Push(currentState.name);
                currentState = currentState.parent;
            }

            txtbox.Text += "Solution Length: " + solution.Count + Environment.NewLine + Environment.NewLine +
                "Solution: ";

            if (solution.Count == 0) return;

            while (solution.Count > 1)
            {
                txtbox.AppendText(solution.Pop() + ", ");
            }
            txtbox.AppendText(solution.Pop() + ".");
        }
    }
}
