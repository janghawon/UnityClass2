using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnector : MonoBehaviour
{
    [SerializeField] private Image _linePrefab;

    private TreeNodeGroup _treeNodeGroup;

    private void Start()
    {
        _treeNodeGroup = GameObject.Find("SkillTreeCanvas/TreeNodeGroup").GetComponent<TreeNodeGroup>();
    }

    public void SetLine()
    {
        Vector3 dir = Vector3.zero;
        foreach(NodeBase selectNode in _treeNodeGroup.NodeList)
        {
            foreach(NodeBase linkNode in selectNode.linkedNodes)
            {
                dir = (linkNode.transform.localPosition - selectNode.transform.localPosition).normalized;
                Transform lineTrans = _linePrefab.transform;
                lineTrans.localRotation = Quaternion.Euler(dir);
                lineTrans.localPosition = (linkNode.transform.localPosition + selectNode.transform.localPosition) / 2;

            }
        }
    }
}
