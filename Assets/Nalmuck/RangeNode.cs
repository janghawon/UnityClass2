using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class RangeNode : ActionNode
    {
        public float detectRange;
        
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            Vector3 dir = brain.targetTrm.position - brain.transform.position;
            if(Vector3.Distance(brain.targetTrm.position, brain.transform.position) < detectRange
            && !Physics.Raycast(brain.transform.position, dir, detectRange, brain.whatIsObstacle))
            {
                return State.SUCCESS;
            }
            return State.FAILURE;
        }
    }
}


