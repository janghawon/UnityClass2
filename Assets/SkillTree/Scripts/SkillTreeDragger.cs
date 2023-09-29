using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeDragger : MonoBehaviour
{
    [SerializeField] private Transform _skillTreeCanvas;
    private Transform _nodeGroup;
    [SerializeField] private float _moveSpeed;

    private Vector3 _saveDragPos;
    private Vector3 _dragDirection;

    private bool _isDragging;

    private void Awake()
    {
        _nodeGroup = _skillTreeCanvas.GetComponentInChildren<TreeNodeGroup>().transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            _dragDirection = (currentMousePosition - _saveDragPos).normalized;
            _saveDragPos = currentMousePosition;
        }

        if(_isDragging)
            _nodeGroup.Translate(_dragDirection * _moveSpeed * Time.deltaTime);
    }
}
