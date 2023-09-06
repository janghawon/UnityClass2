using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ViewCastInfo
{
    public bool hit;
    public Vector3 point;
    public float distance;
    public float angle;
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;
}

public class PlayerFOV : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle; //각도
    public float viewRadius; //반경

    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _enemyFindDelay = 0.3f; //매 프레임마다하면 성능저하

    [SerializeField] private float _meshResolution;
    [SerializeField] private int _edgeResolveIterations;
    [SerializeField] private float _edgeDistanceThreshold;

    private MeshFilter _viewMeshFilter;
    private Mesh _viewMesh;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Awake()
    {
        _viewMeshFilter = transform.Find("ViewVisual").GetComponent<MeshFilter>();
        _viewMesh = new Mesh();
        _viewMesh.name = "Sight_Mesh";
        _viewMeshFilter.mesh = _viewMesh;
    }

    public Vector3 DirFromAngle(float degree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            degree += transform.eulerAngles.y; //로컬회전치라면 글로벌 회전치로 변경한다.
        }

        float rad = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    private void Start()
    {
        StartCoroutine(FindEnemyWithDelay(_enemyFindDelay));
    }

    IEnumerator FindEnemyWithDelay(float delay)
    {
        var time = new WaitForSeconds(delay);
        while (true)
        {
            yield return time;
            FindVisibleEnemies();
        }
    }

    private void FindVisibleEnemies()
    {
        visibleTargets.Clear();

        Collider[] enemiesInView = new Collider[6];

        int cnt = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, enemiesInView, _enemyMask);

        for (int i = 0; i < cnt; i++)
        {
            Transform enemy = enemiesInView[i].transform;
            Vector3 dir = enemy.position - transform.position;
            Vector3 dirToEnemy = dir.normalized;

            if (Vector3.Angle(transform.forward, dirToEnemy) < viewAngle * 0.5f)
            {
                //시야범위 안에 있다면 레이를 쏴서 그안에 아무 장애물도 없다는 것을 알아내야 해
                if (!Physics.Raycast(transform.position, dirToEnemy, dir.magnitude, _obstacleMask))
                {
                    visibleTargets.Add(enemy);
                }
            }
        }

    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private EdgeInfo FindeEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for(int i = 0; i < _edgeResolveIterations; ++i)
        {
            float angle = (minAngle + maxAngle) * 0.5f;
            var castInfo = ViewCast(angle);

            bool distanceExceed = Mathf.Abs(minViewCast.distance - castInfo.distance) > _edgeDistanceThreshold;

            if(castInfo.hit == minViewCast.hit && !distanceExceed)
            {
                minAngle = angle;
                minPoint = castInfo.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = castInfo.point;
            }
        }

        return new EdgeInfo { pointA = minPoint, pointB = maxPoint };
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        Vector3 pos = transform.position;

        List<Vector3> viewPoints = new List<Vector3>();
        var oldViewCastInfo = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
            //Debug.DrawLine(pos, pos + DirFromAngle(angle, true) * viewRadius, Color.red);

            var info = ViewCast(angle);

            if(i > 0)
            {
                bool distanceExceeded = Mathf.Abs(oldViewCastInfo.distance - info.distance) > _edgeDistanceThreshold;

                if (oldViewCastInfo.hit != info.hit || (oldViewCastInfo.hit && info.hit && distanceExceeded))
                {
                    var edge = FindeEdge(oldViewCastInfo, info);
                    if (edge.pointA != Vector3.zero)

                    {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                }
            }

            viewPoints.Add(info.point);
            oldViewCastInfo = info;
        }

        int vertCount = viewPoints.Count + 1; //왜?
        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[(vertCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertCount - 1; i++)
        {
            //정점을 넣어주고
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertCount - 2)
            {
                int tIndex = i * 3;
                triangles[tIndex + 0] = 0;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = i + 2;
            }
        }
        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        //_viewMeshFilter.mesh = _viewMesh;
        _viewMesh.RecalculateNormals();
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewRadius, _obstacleMask))
        {
            return new ViewCastInfo
            {
                hit = true,
                point = hit.point,
                distance = hit.distance,
                angle = globalAngle
            };
        }
        else
        {
            return new ViewCastInfo
            {
                hit = false,
                point = transform.position + dir * viewRadius,
                distance = viewRadius,
                angle = globalAngle
            };
        }
    }
}