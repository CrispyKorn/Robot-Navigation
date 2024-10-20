using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class IDFS<T> : Pathfinder<T>
    {
        LinkedList<State<T>> frontier;

        int maxDepth = 0;

        /// <summary>
        /// A pathfinder that uses Iterative Depth-First Search
        /// </summary>
        public IDFS()
        {
            frontier = new LinkedList<State<T>>();
            searchedNodes = new HashSet<T>();
        }

        /// <summary>
        /// Adds the given list of states to the start of the frontier list
        /// </summary>
        /// <param name="nodes">The list of states to add</param>
        public override void AddArrayToFrontier(List<State<T>> nodes)
        {
            //Iterate through the nodes to add backwards so they stay in order when added to the frontier
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (!searchedNodes.Contains(nodes[i].data))
                {
                    frontier.AddFirst(nodes[i]);
                    discovered++;
                }
            }
        }

        /// <summary>
        /// Uses Depth-First-Search to find a solution, returning an in-order list of steps to the goal
        /// </summary>
        /// <param name="initialState">The state from which to start searching from</param>
        /// <param name="goalState">The state to reach</param>
        /// <param name="scenario">The scenario for the required type</param>
        /// <param name="output">A text field to print the solution</param>
        /// <returns></returns>
        public override LinkedList<State<T>> Search(State<T> initialState, State<T> goalState, Scenario<T> scenario, TextBox output)
        {
            State<T> baseState;
            //For when we haven't already added a list of starting options
            if (initialState != null) baseState = initialState;
            else baseState = frontier.First.Value;
                
            frontier.AddFirst(baseState);

            State<T> currentState = null;
            bool foundGoal = false;

            //While there is still nodes to search
            while (frontier.Count > 0)
            {
                //Set the current state to the first item of the frontier queue, adding it to the set of searched nodes
                currentState = frontier.First.Value;
                frontier.RemoveFirst();
                searchedNodes.Add(currentState.data);
                searched++;

                //Check if this current state is the goal state, if so exit the loop; We're done!
                if (scenario.IsGoalState(currentState, goalState))
                {
                    foundGoal = true;
                    break;
                }

                //Get the depth of the current node by iterating up through its parents
                int currentDepth = 0;
                State<T> tempState = currentState;
                while (tempState.parent != null)
                {
                    tempState = tempState.parent;
                    currentDepth++;
                }

                if (currentDepth <= maxDepth)
                {
                    //Add all possible moves from the current state to the back of the frontier queue
                    AddArrayToFrontier(scenario.GetPossibleMoves(currentState));
                }

                //If we're on the final node of this depth, reset everything and increment the max depth
                if (frontier.Count == 0)
                {
                    maxDepth++;

                    frontier.AddFirst(baseState);
                    searchedNodes.Clear();
                }
            }

            //Solution found! Print it out!
            scenario.PrintSolution(currentState, discovered, searched, output,foundGoal);

            //Produce an in-order list of steps to take to reach the goal and return it
            LinkedList<State<T>> solution = new LinkedList<State<T>>();
            if (!foundGoal) return solution;

            State<T> stateToAdd = currentState;
            while (stateToAdd != null)
            {
                solution.AddFirst(stateToAdd);
                stateToAdd = stateToAdd.parent;
            }

            return solution;
        }
    }
}
