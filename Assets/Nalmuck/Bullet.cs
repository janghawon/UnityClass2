using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    bool isFire;
    Vector3 direction;
    public void Fire(Vector3 dir)
    {
        dir.y = 0;
        direction = dir;
        isFire = true;
        Destroy(gameObject, 2);
    }

    private void Update()
    {
        if(isFire)
            transform.position += direction * _speed * Time.deltaTime;
    }
}
