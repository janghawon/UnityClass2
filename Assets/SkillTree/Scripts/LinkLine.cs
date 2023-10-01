using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkLine : MonoBehaviour
{
    public NodeBase LinkedNode;
    [SerializeField] private float _limitTime;
    private Image _thisLine;
    public bool isLinkingStart;
    private bool _isFirst = true;

    private void Awake()
    {
        _thisLine = GetComponent<Image>();
    }

    private void Update()
    {
        if(isLinkingStart)
        {
            if(_isFirst)
            {
                StartCoroutine(TimerCo());
                _isFirst = false;
            }
            _thisLine.fillAmount = Mathf.Lerp(_thisLine.fillAmount, 1, Time.deltaTime / _limitTime);
        }
    }

    IEnumerator TimerCo()
    {
        yield return new WaitForSeconds(_limitTime + 0.5f);
        isLinkingStart = false;
        LinkedNode.UnLockNode();
    }
}
