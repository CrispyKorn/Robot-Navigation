using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNav
{
    internal class State<T>
    {
        /// <summary>
        /// The contents of the state
        /// </summary>
        public T data { get; private set; }

        /// <summary>
        /// The parent of this state
        /// </summary>
        public State<T> parent { get; private set; }

        /// <summary>
        /// The name to describe the state
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The cost to reach the goal
        /// </summary>
        public float hCost { get; private set; }

        /// <summary>
        /// The cost to reach this position
        /// </summary>
        public float gCost { get; private set; }

        /// <summary>
        /// Contains all data relevant to a world-state
        /// </summary>
        /// <param name="_data">The data that defines the state</param>
        /// <param name="_parent">The parent state of this state</param>
        /// <param name="_name">The name of this state, used when printing out states</param>
        /// <param name="_hCost">The cost to reach the goal from this state</param>
        /// <param name="_gCost">The cost to reach this state from the initial state</param>
        public State(T _data, State<T> _parent, string _name, float _hCost = 0f, float _gCost = 0f)
        {
            data = _data;
            parent = _parent;
            name = _name;
            hCost = _hCost;
            gCost = _gCost;
        }
    }
}
