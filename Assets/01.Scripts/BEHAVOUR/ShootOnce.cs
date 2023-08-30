using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Action("MyAction/ShhoOnce")]
[Help("½´ÆÃ 1È¸")]
public class ShootOnce : BasePrimitiveAction
{
    [InParam("ShootPoint")] public Transform shootPoint;
    [InParam("Prefab")] public Bullet bullet;
    [InParam("velocity", DefaultValue = 30f)] public float velocity;
    [InParam("Body")] public Transform body;
    [InParam("Target")] public GameObject target;

    public override TaskStatus OnUpdate()
    {
        Vector3 dir = target.transform.position - body.position;
        body.rotation = Quaternion.LookRotation(dir.normalized);
        var newBullet = GameObject.Instantiate(bullet, shootPoint.position, Quaternion.identity);
        newBullet.Fire(shootPoint.forward * velocity);
        return TaskStatus.COMPLETED;
    }
}
