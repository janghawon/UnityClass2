using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnector : MonoBehaviour
{
    [SerializeField] private Image _linePrefab;

    private TreeNodeGroup _treeNodeGroup;
    private Transform _lineGroup;

    private void Start()
    {
        SetLine();
    }

    public void SetLine()
    {
        _treeNodeGroup = GameObject.Find("SkillTreeCanvas/TreeNodeGroup").GetComponent<TreeNodeGroup>();
        _lineGroup = _treeNodeGroup.transform.Find("LineGroup");

        Vector3 dir;
        float x1, x2;
        float y1, y2;

        foreach(NodeBase selectNode in _treeNodeGroup.NodeList)
        {
            x1 = selectNode.transform.localPosition.x;
            y1 = selectNode.transform.localPosition.y;
            
            foreach (NodeBase linkNode in selectNode.linkedNodes)
            {
                x2 = linkNode.transform.localPosition.x;
                y2 = linkNode.transform.localPosition.y;

                
                RectTransform lineTrans = Instantiate(_linePrefab, _lineGroup).rectTransform;
                LinkLine ll = lineTrans.GetComponent<LinkLine>();
                ll.LinkedNode = linkNode;
                selectNode.linklines.Add(ll);

                Vector2 selectNodePos = new Vector2(x1, y1);
                Vector2 linkNodePos   = new Vector2(x2, y2);

                dir = (linkNodePos - selectNodePos).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                lineTrans.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                lineTrans.localPosition = (linkNodePos + selectNodePos) / 2;
                lineTrans.sizeDelta = new Vector2(20, Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)));
            }
        }
    }
}
