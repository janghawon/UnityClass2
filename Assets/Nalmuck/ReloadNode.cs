using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class ReloadNode : ActionNode
    {
        public float reloadTime;
        private float currentTime;
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {

        }

        protected override State OnUpdate()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= reloadTime)
            {
                currentTime = 0;
                brain.bulletCount = brain.maxBulletCount;
                return State.FAILURE;
            }
            return State.RUNNING;
        }
    }
}


