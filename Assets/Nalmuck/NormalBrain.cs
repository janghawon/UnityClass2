using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalBrain : EnemyBrain
{
    [SerializeField] private Transform _firePos;
    [SerializeField] private Bullet _bulletPrefab;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public override void Attack()
    {
        Vector3 direction = targetTrm.position - transform.position;
        direction.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
        
        var bullet = Instantiate(_bulletPrefab, _firePos.position, Quaternion.identity);
        bullet.Fire(direction);
    }
}
