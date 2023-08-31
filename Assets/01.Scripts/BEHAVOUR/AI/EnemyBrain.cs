using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    [SerializeField] protected Transform _targetTrm;

    protected NavMeshAgent _navAgent;
    public NavMeshAgent navAgent => _navAgent;

    private UIBar _uiBar;
    private Camera _camera;
    private Coroutine _coroutine;

    public NodeActionCode currentCode;

    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _uiBar = transform.Find("Status").GetComponent<UIBar>();
        _camera = Camera.main;
    }

    protected virtual void LateUpdate()
    {
        _uiBar.LookToCamera();
    }

    public void TryToTalk(string text, float timer = 1)
    {
        _uiBar.SetText(text);
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(PanelFade(timer));
    }

    IEnumerator PanelFade(float timer)
    {
        _uiBar.IsOn = true;
        yield return new WaitForSeconds(timer);
        _uiBar.IsOn = false;
    }

    public abstract void Attack();
}
