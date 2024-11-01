

namespace RobotNav
{
    internal class IDFS<T> : Pathfinder<T>
    {
        LinkedList<State<T>> _frontier = new();

        int _maxDepth = 0;

        /// <summary>
        /// A pathfinder that uses Iterative Depth-First Search
        /// </summary>
        public IDFS()
        {
            
        }

        /// <summary>
        /// Adds the given list of states to the start of the frontier list
        /// </summary>
        /// <param name="nodes">The list of states to add</param>
        public override void AddArrayToFrontier(List<State<T>> nodes)
        {
            //Iterate through the nodes to add backwards so they stay in order when added to the frontier
            for (var i = nodes.Count - 1; i >= 0; i--)
            {
                if (!_searchedNodes.Contains(nodes[i].Data))
                {
                    _frontier.AddFirst(nodes[i]);
                    _discovered++;
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
            // For when we haven't already added a list of starting options
            if (initialState is not null) baseState = initialState;
            else baseState = _frontier.First.Value;
                
            _frontier.AddFirst(baseState);
            LinkedList<State<T>> solution = new();

            if (_frontier.Count == 0 || _frontier.First is null) return solution;

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

                // Get the depth of the current node by iterating up through its parents
                var currentDepth = 0;
                var tempState = currentState;
                while (tempState.Parent != null)
                {
                    tempState = tempState.Parent;
                    currentDepth++;
                }

                if (currentDepth <= _maxDepth)
                {
                    // Add all possible moves from the current state to the back of the frontier queue
                    AddArrayToFrontier(scenario.GetPossibleMoves(currentState));
                }

                // If we're on the final node of this depth, reset everything and increment the max depth
                if (_frontier.Count == 0)
                {
                    _maxDepth++;

                    _frontier.AddFirst(baseState);
                    _searchedNodes.Clear();
                }
            } while (_frontier.Count > 0) ; // While there is still nodes to search

            // Solution found! Print it out!
            scenario.PrintSolution(currentState, _discovered, _searched, output,foundGoal);

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
