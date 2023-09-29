using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class NodeBase : MonoBehaviour
{
    [SerializeField] private NodeInfoSO _nodeSO;
    private TreeNodeGroup _treeNodeGroup;
    public bool isUnlock;
    private Image _thisNodeStateVisual;
    [SerializeField] private NodeBase[] _linkedNodes;

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
        seq.Append(transform.DOShakePosition(1, 10));
        seq.AppendCallback(() =>
        {
            _thisNodeStateVisual.sprite = _nodeSO.nodeImage;
        });
    }

    public void ActiveNode()
    {
        _thisNodeStateVisual.material = _treeNodeGroup.ActiveNodeMat;
        foreach (NodeBase node in _linkedNodes)
        {
            node.UnLockNode();
        }
    }

    public abstract void ApplyNodeEffect();
}
