using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NodeBase : MonoBehaviour
{
    [SerializeField] protected NodeInfoSO _nodeSO;
    private TreeNodeGroup _treeNodeGroup;
    public bool isUnlock;
    private Image _thisNodeStateVisual;
    public NodeBase[] linkedNodes;
    public List<LinkLine> linklines = new List<LinkLine>();

    private void Awake()
    {
        _treeNodeGroup = GetComponentInParent<TreeNodeGroup>();
        _thisNodeStateVisual = transform.Find("StateVisual").GetComponent<Image>();
    }

    public void LockNode()
    {
        isUnlock = false;
        _thisNodeStateVisual.sprite = _treeNodeGroup.LockTexture;
    }

    public void UnLockNode()
    {
        isUnlock = true;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOShakePosition(1, 6));
        seq.AppendCallback(() =>
        {
            _thisNodeStateVisual.sprite = _nodeSO.nodeImage;
        });
    }

    public void ActiveNode()
    {
        _thisNodeStateVisual.material = _treeNodeGroup.ActiveNodeMat;
        ApplyNodeEffect();
    }

    public virtual void ApplyNodeEffect()
    {
        foreach (LinkLine ll in linklines)
        {
            ll.isLinkingStart = true;
        }
    }
}
