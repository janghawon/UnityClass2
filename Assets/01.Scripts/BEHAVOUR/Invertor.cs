using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Invertor : Node
    {
        protected Node _node;

        public Invertor(Node node)
        {
            _node = node;
        }

        public override NodeState Evaluate()
        {
            switch(_node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    break;
                case NodeState.SUCESS:
                    _nodeState = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.SUCESS;
                    break;
            }

            return _nodeState;
        }
    }
}


