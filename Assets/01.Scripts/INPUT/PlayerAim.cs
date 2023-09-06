using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _whatisGround;

    private Camera _maincam;
    [SerializeField] private Transform _rotateTrm;

    private void Start()
    {
        _maincam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector2 mousePos = _inputReader.AimPosition;
        Ray ray = _maincam.ScreenPointToRay(mousePos);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, _maincam.farClipPlane, _whatisGround))
        {
            Vector3 worldMousePos = hitInfo.point;
            Vector3 dir = (worldMousePos - transform.position);
            dir.y = 0;
            _rotateTrm.rotation = Quaternion.LookRotation(dir);
        }
    }
}
