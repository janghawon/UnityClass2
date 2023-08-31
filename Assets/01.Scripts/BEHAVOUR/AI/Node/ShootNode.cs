using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private NavMeshAgent _agent;
    private EnemyBrain _brain;
    private float _coolTime = 0;
    private float _lastFireTime = 0;

    public ShootNode(NavMeshAgent agent, EnemyBrain brain, float coolTime)
    {
        _agent = agent;
        _brain = brain;
        _coolTime = coolTime;
        _nodeState = NodeState.SUCESS;
        _code = NodeActionCode.Shoot;
    }

    public override NodeState Evaluate()
    {
        _agent.isStopped = true;
        if(_brain.currentCode != _code)
        {
            _brain.TryToTalk("Attack!!!", 1f);
            _brain.currentCode = _code;
        }

        if(_lastFireTime + _coolTime <= Time.time)
        {
            _brain.TryToTalk("Attack", 1f);
            _brain.Attack();
            _lastFireTime = Time.time;
        }
        return NodeState.RUNNING;
    }
}
