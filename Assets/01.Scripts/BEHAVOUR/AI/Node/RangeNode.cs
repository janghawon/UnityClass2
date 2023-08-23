using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float _range;
    private Transform _target;
    private Transform _transform;

    public RangeNode(float range, Transform target, Transform transform)
    {
        _range = range;
        _target = target;
        _transform = transform;
    }
    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_transform.position, _target.position);
        return distance < _range ? NodeState.SUCESS : NodeState.FAILURE;
    }
}
