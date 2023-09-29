using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BTVisual
{
    public class ChaseNode : ActionNode
    {
        public float AttackRange;
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            brain.nav.SetDestination(brain.targetTrm.position);
            if(Vector3.Distance(brain.targetTrm.position, brain.transform.position) < AttackRange)
            {
                brain.nav.SetDestination(brain.transform.position);
                return State.SUCCESS;
            }
            else
            {
                return State.RUNNING;
            }
            
        }
    }
}


