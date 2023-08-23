using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    SUCESS = 1,
    FAILURE = 2,
    RUNNING = 3
}

public abstract class Node
{
    protected NodeState _nodeState;
    public NodeState NodeState => _nodeState;

    public abstract NodeState Evaluate();
}
