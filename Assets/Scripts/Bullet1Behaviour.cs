using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1Behaviour : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private LayerMask whatDestroyBullet;
    [SerializeField] private float destroyTime = 3f;

    [Header("Normal Bullet Stats")]
    [SerializeField] private float bulletVelocity = 5f;
    [SerializeField] private float normalBulletDamage = 1.0f;

    [Header("Physics Bullet Stats")]
    [SerializeField] private float physicsBulletVelocity = 1f;
    [SerializeField] private float physicsBulletDamage = 100f;


    private Rigidbody2D rb;
    private float damage;

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();

        InitializeBulletStat();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeBulletStat()
    {
        if (bulletType == BulletType.Normal)
        {
            BulletVelocity();
            damage =  normalBulletDamage;
        }
        else if (bulletType == BulletType.Physics)
        {
            PhysicsBulletVelocity();
            damage = physicsBulletDamage;
        }
    }

    private void BulletVelocity()
    {
        rb.velocity = transform.up * bulletVelocity;
    }

    private void PhysicsBulletVelocity()
    {
        rb.velocity = transform.up * physicsBulletVelocity;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroyBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
            if (iDamagable != null)
            {
                iDamagable.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
