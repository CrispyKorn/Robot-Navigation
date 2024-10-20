using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class AStar<T> : Pathfinder<T>
    {
        PriorityQueue<State<T>, float> frontier;

        /// <summary>
        /// A pathfinder that uses A* Search
        /// </summary>
        public AStar()
        {
            frontier = new PriorityQueue<State<T>, float>();
            searchedNodes = new HashSet<T>();
        }

        /// <summary>
        /// Adds the given list of states to the frontier priority queue
        /// </summary>
        /// <param name="nodes">The list of states to add</param>
        public override void AddArrayToFrontier(List<State<T>> nodes)
        {
            foreach (State<T> node in nodes)
            {
                if (!searchedNodes.Contains(node.data))
                {
                    frontier.Enqueue(node, node.hCost + node.gCost);
                    discovered++;
                }
            }
        }

        /// <summary>
        /// Uses A-Star Search to find a solution, returning an in-order list of steps to the goal
        /// </summary>
        /// <param name="initialState">The state from which to start searching from</param>
        /// <param name="goalState">The state to reach</param>
        /// <param name="scenario">The scenario for the required type</param>
        /// <param name="output">A text field to print the solution</param>
        /// <returns></returns>
        public override LinkedList<State<T>> Search(State<T> initialState, State<T> goalState, Scenario<T> scenario, TextBox output)
        {
            //For when we haven't already added a list of starting options
            if (initialState != null) frontier.Enqueue(initialState, initialState.hCost);

            State<T> currentState = null;
            bool foundGoal = false;

            //While there is still nodes to search
            while (frontier.Count > 0)
            {
                //Set the current state to the first item of the frontier queue, adding it to the set of searched nodes
                currentState = frontier.Dequeue();
                searchedNodes.Add(currentState.data);
                searched++;

                //Check if this current state is the goal state, if so exit the loop; We're done!
                if (scenario.IsGoalState(currentState, goalState))
                {
                    foundGoal = true;
                    break;
                }

                //Add all possible moves from the current state to the back of the frontier queue
                AddArrayToFrontier(scenario.GetPossibleMoves(currentState));
            }

            //Solution found! Print it out!
            scenario.PrintSolution(currentState, discovered, searched, output, foundGoal);

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