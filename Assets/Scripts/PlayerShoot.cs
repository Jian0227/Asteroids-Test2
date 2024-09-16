using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet1")]
    public Bullet1 bullet1;
    public Transform bulletSpawnPoint;
    public float timeBetweenShooting;
    public bool readyToShoot;

    [Header("Bullet2")]
    public Bullet2 bullet2;
    public Transform bullet2SpawnPoint;
    public int bullet2Amount;
    [SerializeField] private float startAngle = 90f, endAngle = 270f;
    private Quaternion bullet2Rotation;
    public float timeBetweenShooting2;
    public bool readyToShoot2;

    [Header("Bullet3")]
    public Bullet3 bullet3;
    public Transform bullet3SpawnPoint;
    public int bullet3Amount;
    [SerializeField] private float startAngle2 = 90f, endAngle2 = 270f;
    private Quaternion bullet3Rotation;

    private BulletSpawner bulletSpawner;


    // Start is called before the first frame update
    private void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        readyToShoot = true;
        readyToShoot2 = true;

        InvokeRepeating("FireBullet3", 0f, 2f);
    }

    // Update is called once per frame
    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetKey(KeyCode.Mouse0) && readyToShoot)
        {
            //bulletInst = Instantiate(bullet1, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            FireBullet1();
        }
        else if (Input.GetKey(KeyCode.Mouse1) && readyToShoot2)
        {
            FireBullet2();
        }
    }

    private void FireBullet1()
    {
        readyToShoot = false;

        bulletSpawner._pool.Get();

        Invoke("ResetShot", timeBetweenShooting);

    }

    private void FireBullet2()
    {
        readyToShoot2 = false;

        // Calculate the center angle (middle of the arc)
        float middleAngle = (startAngle + endAngle) / 2f;

        // Create a list of angles to ensure the order alternates between left and right
        List<float> angles = new List<float>();
        angles.Add(middleAngle);

        float angleStep = (endAngle - startAngle) / (bullet2Amount - 1);

        for (int i = 1; i <= bullet2Amount / 2; i++)
        {
            // Alternate between left and right around the middle angle
            float leftAngle = middleAngle - (angleStep * i);
            float rightAngle = middleAngle + (angleStep * i);

            angles.Add(leftAngle);
            angles.Add(rightAngle);
        }

        // Spawn the bullets according to the calculated angles
        for (int i = 0; i < bullet2Amount; i++)
        {
            Bullet2 bullet2 = bulletSpawner._pool2.Get();

            bullet2Rotation = Quaternion.Euler(0, 0, angles[i]);

            bullet2.transform.position = bullet2SpawnPoint.position;
            bullet2.transform.rotation = bullet2SpawnPoint.rotation * bullet2Rotation;

            Vector2 bulletDirection2 = bullet2.transform.up;

            bullet2.GetComponent<Rigidbody2D>().velocity = bulletDirection2 * bullet2.bullet2Velocity;
        }

        Invoke("ResetShot2", timeBetweenShooting2);
    }


    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ResetShot2()
    {
        readyToShoot2 = true;
    }

    private void FireBullet3()
    {
        float angleStep = (endAngle2 - startAngle2) / bullet3Amount;
        float currentAngle = startAngle2;

        for (int i = 0; i < bullet3Amount; i++)
        {
            //float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            //float bulDirY = transform.position.y + Mathf.Sin((angle * Mathf.PI) / 180f);

            //Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            //Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            Bullet3 bullet3 = bulletSpawner._pool3.Get();

            bullet3Rotation = Quaternion.Euler(0, 0, currentAngle);

            bullet3.transform.position = bullet3SpawnPoint.position;
            bullet3.transform.rotation = bullet3SpawnPoint.rotation * bullet3Rotation;

            Vector2 bulletDirection3 = bullet3.transform.up; 

            bullet3.GetComponent<Rigidbody2D>().velocity = bulletDirection3 * bullet3.bullet3Velocity;

            currentAngle += angleStep;




        }
    }

    
}
