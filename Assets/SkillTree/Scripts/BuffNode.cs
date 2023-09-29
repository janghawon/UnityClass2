using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffNode : NodeBase
{
    public override void ApplyNodeEffect()
    {
        Debug.Log(name);
    }
}
