using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Bullet1Buff")]
public class Bullet1Buff : PowerUpEfffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Bullet1>().normalBulletDamage += amount;
    }
}
