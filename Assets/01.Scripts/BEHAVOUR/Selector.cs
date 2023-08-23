using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : Node
    {
        protected List<Node> _nodes = new List<Node>();

        public Selector(List<Node> nodes)
        {
            _nodes = nodes;
        }
        public override NodeState Evaluate()
        {
            foreach(var n in _nodes)
            {
                switch(n.Evaluate())
                {
                    case NodeState.RUNNING:
                        _nodeState = NodeState.RUNNING;
                        return _nodeState;
                    case NodeState.SUCESS:
                        _nodeState = NodeState.SUCESS;
                        return _nodeState;
                    case NodeState.FAILURE:
                        break;
                    default:
                        break;
                }
            }

            _nodeState = NodeState.FAILURE;
            return _nodeState;
        }
    }
}


