using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class AsteroidManager : MonoBehaviour
{
    [Header("Asteroid Settings")]
    public GameObject asteroidPrefab; 
    public float spawnRate = 1.0f; 
    public int minAsteroids = 1; 
    public int maxAsteroids = 5; 
    public float asteroidSpeed = 5f; 

    public Camera mainCamera;
    private float screenLeft, screenRight, screenTop, screenBottom;
    public Vector2 spawnPosition;
    private ObjectPool<GameObject> _asteroidPool;

    public static AsteroidManager instance;

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    
    void Start()
    {
        //mainCamera = Camera.main;
        GetScreenBounds();

        
        _asteroidPool = new ObjectPool<GameObject>(CreateAsteroid, OnTakeFromPool, OnReturnToPool, OnDestroyAsteroid, true, 100, 200);

        
        //InvokeRepeating("SpawnAsteroids", 0f, spawnRate);
    }

    
    private GameObject CreateAsteroid()
    {
        GameObject asteroid = Instantiate(asteroidPrefab);
        asteroid.SetActive(false);
        return asteroid;
    }

    private void OnTakeFromPool(GameObject asteroid)
    {
        asteroid.SetActive(true);
    }

    private void OnReturnToPool(GameObject asteroid)
    {
        asteroid.SetActive(false);
    }

    private void OnDestroyAsteroid(GameObject asteroid)
    {
        Destroy(asteroid);
    }

    
    void GetScreenBounds()
    {
        screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).x;
        screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.transform.position.z)).x;
        screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, mainCamera.transform.position.z)).y;
        screenBottom = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z)).y;
    }

    
    void SpawnAsteroids()
    {
        int asteroidCount = Random.Range(minAsteroids, maxAsteroids + 1); 
        for (int i = 0; i < asteroidCount; i++)
        {
            spawnPosition = GetRandomSpawnPosition(); 
            GameObject asteroid = _asteroidPool.Get();
            asteroid.transform.position = spawnPosition;
            asteroid.transform.rotation = Quaternion.identity;

            Vector2 moveDirection = GetMoveDirection(spawnPosition);
            asteroid.GetComponent<Rigidbody2D>().velocity = moveDirection * asteroidSpeed;
        }
    }

    
    Vector2 GetRandomSpawnPosition()
    {
        int side = Random.Range(0, 4); 
        switch (side)
        {
            case 0: // Left
                return new Vector2(screenLeft, Random.Range(screenBottom, screenTop));
            case 1: // Right
                return new Vector2(screenRight, Random.Range(screenBottom, screenTop));
            case 2: // Top
                return new Vector2(Random.Range(screenLeft, screenRight), screenTop);
            case 3: // Bottom
                return new Vector2(Random.Range(screenLeft, screenRight), screenBottom);
            default:
                return Vector2.zero;
        }
    }

    
    Vector2 GetMoveDirection(Vector2 spawnPosition)
    {
        Vector2 screenCenter = new Vector2((screenLeft + screenRight) / 2, (screenBottom + screenTop) / 2);
        return (screenCenter - spawnPosition).normalized; 
    }

    
    public void ReturnAsteroidToPool(GameObject asteroid)
    {
        _asteroidPool.Release(asteroid);
    }

    public void ClearAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject asteroid in asteroids)
        {
            ReturnAsteroidToPool (asteroid);
        }
    }

}
