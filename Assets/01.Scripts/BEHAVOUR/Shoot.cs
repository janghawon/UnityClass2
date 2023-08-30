using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("MyAction/Shoot")]
public class Shoot : ShootOnce
{
    [InParam("cooltime", DefaultValue = 1)] public float cooltime;
    public float _lastFiretime;

    public override TaskStatus OnUpdate()
    {
        if(Time.time < cooltime + _lastFiretime)
        {
            return TaskStatus.RUNNING;
        }
        base.OnUpdate();
        _lastFiretime = Time.time;
        return TaskStatus.RUNNING;
    }
}
