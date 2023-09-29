using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBrain : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform targetTrm;
    //기타 등등 브레인에서 필요한 것들을 여기 넣어라
    public LayerMask whatIsObstacle;
    public int maxBulletCount = 8;
    public int bulletCount;
    public abstract void Attack();
}
