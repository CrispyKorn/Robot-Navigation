

namespace RobotNav
{
    internal abstract class Pathfinder<T>
    {
        protected HashSet<T> _searchedNodes = new();
        protected int _discovered = 0, _searched = 0;

        /// <summary>
        /// Adds the given list of states to the frontier
        /// </summary>
        /// <param name="nodes">The list of states to add</param>
        protected abstract void AddArrayToFrontier(List<State<T>> nodes);

        /// <summary>
        /// Searches to find a solution, returning an in-order list of steps to the goal
        /// </summary>
        /// <param name="initialState">The state from which to start searching from</param>
        /// <param name="goalState">The state to reach</param>
        /// <param name="scenario">The scenario for the required type</param>
        /// <param name="output">A text field to print the solution</param>
        /// <returns></returns>
        public abstract LinkedList<State<T>> Search(State<T> initialState, State<T> goalState, Scenario<T> scenario, TextBox output);
    }
}
