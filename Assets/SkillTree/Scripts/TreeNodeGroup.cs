using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNodeGroup : MonoBehaviour
{
    public Material ActiveNodeMat;
    public Sprite LockTexture;
    public List<NodeBase> NodeList = new List<NodeBase>();

    private void Awake()
    {
        foreach(Transform node in transform)
        {
            if(node.TryGetComponent<NodeBase>(out NodeBase n))
                NodeList.Add(n);
        }
    }
}
