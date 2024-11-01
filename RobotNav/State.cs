

namespace RobotNav
{
    internal class State<T>
    {
        private readonly T _data;
        private readonly State<T> _parent;
        private readonly string _name;
        private readonly float _hCost;
        private readonly float _gCost;

        /// <summary>
        /// The contents of the state
        /// </summary>
        public T Data { get => _data; }

        /// <summary>
        /// The parent of this state
        /// </summary>
        public State<T> Parent { get => _parent; }

        /// <summary>
        /// The name to describe the state
        /// </summary>
        public string Name { get => _name; }

        /// <summary>
        /// The cost to reach the goal
        /// </summary>
        public float HCost { get => _hCost; }

        /// <summary>
        /// The cost to reach this position
        /// </summary>
        public float GCost { get => _gCost; }

        /// <summary>
        /// Contains all data relevant to a world-state
        /// </summary>
        /// <param name="data">The data that defines the state</param>
        /// <param name="parent">The parent state of this state</param>
        /// <param name="name">The name of this state, used when printing out states</param>
        /// <param name="hCost">The cost to reach the goal from this state</param>
        /// <param name="gCost">The cost to reach this state from the initial state</param>
        public State(T data, State<T> parent, string name, float hCost = 0f, float gCost = 0f)
        {
            _data = data;
            _parent = parent;
            _name = name;
            _hCost = hCost;
            _gCost = gCost;
        }
    }
}
