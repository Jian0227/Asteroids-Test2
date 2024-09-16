using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Asteroids : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public Asteroid asteroidPrefab; // The asteroid to spawn
    public float spawnRate = 1.0f; // How often asteroids will spawn
    public int minAsteroids = 1; // Minimum number of asteroids per spawn
    public int maxAsteroids = 5; // Maximum number of asteroids per spawn
    public float asteroidSpeed = 5f; // Speed of the asteroids

    private Camera mainCamera;
    private float screenLeft, screenRight, screenTop, screenBottom;
    public Vector2 spawnPosition;
    public Quaternion spawnRotation;

    public ObjectPool<Asteroid> _asteroidPool;


    // Start is called before the first frame update
    //void Start()
    //{
    //    _asteroidPool = new ObjectPool<Asteroid>(CreateAsteroid, OnTakeAsteroidFromPool, OnReturnAsteroidToPool, OnDestroyAsteroid, true, 1000, 2000);


    //    mainCamera = Camera.main;
    //    GetScreenBounds();
    //    InvokeRepeating("SpawnAsteroids", 0f, spawnRate);
    //}

    //private Asteroid CreateAsteroid()
    //{
    //    Asteroid asteroid = Instantiate(asteroidPrefab, playerShoot.bulletSpawnPoint.position, playerShoot.bulletSpawnPoint.transform.rotation);

    //    bullet.SetPool(_pool);

    //    return bullet;
    //}

    //// Get the screen boundaries
    //void GetScreenBounds()
    //{
    //    screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
    //    screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
    //    screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.transform.position.z)).y;
    //    screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).y;
    //}

    //// Spawn asteroids randomly along the screen edges
    //void SpawnAsteroids()
    //{
    //    int asteroidCount = Random.Range(minAsteroids, maxAsteroids + 1); // Random asteroid count
    //    for (int i = 0; i < asteroidCount; i++)
    //    {
    //        spawnRotation = Quaternion.identity;
    //        spawnPosition = GetRandomSpawnPosition(); // Random position on the screen edge
    //        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, spawnRotation);
    //        Vector2 moveDirection = GetMoveDirection(spawnPosition);
    //        asteroid.GetComponent<Rigidbody2D>().velocity = moveDirection * asteroidSpeed;
    //    }
    //}

    //// Get a random position on one of the screen's borders
    //Vector2 GetRandomSpawnPosition()
    //{
    //    int side = Random.Range(0, 4); // 0: Left, 1: Right, 2: Top, 3: Bottom
    //    switch (side)
    //    {
    //        case 0: // Left
    //            return new Vector2(screenLeft, Random.Range(screenBottom, screenTop));
    //        case 1: // Right
    //            return new Vector2(screenRight, Random.Range(screenBottom, screenTop));
    //        case 2: // Top
    //            return new Vector2(Random.Range(screenLeft, screenRight), screenTop);
    //        case 3: // Bottom
    //            return new Vector2(Random.Range(screenLeft, screenRight), screenBottom);
    //        default:
    //            return Vector2.zero;
    //    }
    //}

    //// Determine the direction the asteroid will move in based on its spawn position
    //Vector2 GetMoveDirection(Vector2 spawnPosition)
    //{
    //    Vector2 screenCenter = new Vector2((screenLeft + screenRight) / 2, (screenBottom + screenTop) / 2);
    //    return (screenCenter - spawnPosition).normalized; // Move towards the center
    //}
}
