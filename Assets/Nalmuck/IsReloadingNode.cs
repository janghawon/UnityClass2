using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTVisual;

public class IsReloadingNode : ActionNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return brain.bulletCount == 0 ? State.SUCCESS : State.FAILURE;
    }
}
