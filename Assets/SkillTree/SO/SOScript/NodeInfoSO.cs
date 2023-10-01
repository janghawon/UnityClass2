using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/NodeSO")]
public class NodeInfoSO : ScriptableObject
{
    public string nodeName;
    public string nodeInfo;
    public int nodePrice;
    public Sprite nodeImage;
}
