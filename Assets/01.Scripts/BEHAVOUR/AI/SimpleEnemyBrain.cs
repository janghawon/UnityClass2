using BehaviourTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBrain : EnemyBrain
{
    private Node _topNode;
    private NodeState _beforeTopState;

    private void Start()
    {
        ConstructAITree();
    }

    private void ConstructAITree()
    {
        Transform me = transform;
        RangeNode shootRange = new RangeNode(3f, _targetTrm, me);
        ShootNode shootNode = new ShootNode(_navAgent, this, 1.5f);
        Sequence shotSeq = new Sequence(new List<Node> { shootRange, shootNode });

        RangeNode chaseRange = new RangeNode(10f, _targetTrm, me);
        ChaseNode chaseNode = new ChaseNode(_targetTrm, _navAgent, this);
        Sequence chaseSeq = new Sequence(new List<Node> { chaseRange, chaseNode });

        _topNode = new Selector(new List<Node> { shotSeq, chaseSeq });
    }

    private void Update()
    {
        _topNode.Evaluate();
        if(_topNode.NodeState == NodeState.FAILURE && _beforeTopState != NodeState.FAILURE)
        {
            TryToTalk("Nothing to do");
        }
        _beforeTopState = _topNode.NodeState;
    }
}
