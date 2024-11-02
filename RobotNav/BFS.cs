

namespace RobotNav
{
    internal class BFS<T> : Pathfinder<T>
    {
        LinkedList<State<T>> _frontier = new();

        /// <summary>
        /// A pathfinder that uses Breadth-First Search
        /// </summary>
        public BFS()
        {
            
        }

        /// <summary>
        /// Adds the given list of states to the end of the frontier list
        /// </summary>
        /// <param name="nodes">The list of states to add</param>
        protected override void AddArrayToFrontier(List<State<T>> nodes)
        {
            foreach (var node in nodes)
            {
                if (node is not null && !_searchedNodes.Contains(node.Data))
                {
                    _frontier.AddLast(node);
                    _discovered++;
                }
            }
        }

        /// <summary>
        /// Uses Breadth-First-Search to find a solution, returning an in-order list of steps to the goal
        /// </summary>
        /// <param name="initialState">The state from which to start searching from</param>
        /// <param name="goalState">The state to reach</param>
        /// <param name="scenario">The scenario for the required type</param>
        /// <param name="output">A text field to print the solution</param>
        /// <returns></returns>
        public override LinkedList<State<T>> Search(State<T> initialState, State<T> goalState, Scenario<T> scenario, TextBox output)
        {
            LinkedList<State<T>> solution = new();

            if (initialState is not null) _frontier.AddLast(initialState);
            else return solution;

            State<T> currentState;
            var foundGoal = false;

            do
            {
                // Set the current state to the first item of the frontier queue, adding it to the set of searched nodes
                currentState = _frontier.First.Value;
                _frontier.RemoveFirst();
                _searchedNodes.Add(currentState.Data);
                _searched++;

                // Check if this current state is the goal state, if so exit the loop; We're done!
                if (scenario.IsGoalState(currentState, goalState))
                {
                    foundGoal = true;
                    break;
                }

                // Add all possible moves from the current state to the back of the frontier queue
                AddArrayToFrontier(scenario.GetPossibleMoves(currentState));
            } while (_frontier.Count > 0); // While there is still nodes to search

            // Print out solution
            scenario.PrintSolution(currentState, _discovered, _searched, output, foundGoal);

            // Produce an in-order list of steps to take to reach the goal and return it
            if (!foundGoal) return solution;

            do
            {
                solution.AddFirst(currentState);
                currentState = currentState.Parent;
            } while (currentState is not null);

            return solution;
        }
    }
}
