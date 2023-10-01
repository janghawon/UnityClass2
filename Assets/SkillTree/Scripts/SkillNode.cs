using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNode : NodeBase
{
    private void Start()
    {
        if(isUnlock)
        {
            UnLockNode();
        }
        else
        {
            LockNode();
        }
    }

    public override void ApplyNodeEffect()
    {
        base.ApplyNodeEffect();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GoldManager.Instance.CheckSpendGold(_nodeSO.nodePrice))
            {
                GoldManager.Instance.Gold -= _nodeSO.nodePrice;
                ActiveNode();
            }
        }
    }
}
