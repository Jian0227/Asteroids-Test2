using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //public GameObject gameOver;
    //public GameObject[] heart;
    public int health = 10;
    //public bool stillCollide = false;
    // Start is called before the first frame update
    public TMP_Text heartText;
    public string sceneName;
    //[SerializeField] private LayerMask whatDamagePlayer;

    public static PlayerHealth instance;


    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject); // Keep this object alive between scenes
    //    }
    //    else
    //    {
    //        Destroy(gameObject); // Destroy duplicate instance
    //    }
    //}

    void Start()
    {
        //gameOver.gameObject.SetActive(false);
        //heart[heart.Length - 1].gameObject.SetActive(true);
    }

    //public void ApplyLoadedData(int loadedHealth)
    //{
    //    health = loadedHealth;
    //    Debug.Log("Loaded Health: " + health);
    //}

    // Update is called once per frame
    void Update()
    {
        //if (health == 8)
        //{
        //    heart[8].gameObject.SetActive(false);
        //}
        //else if (health == 7)
        //{
        //    heart[7].gameObject.SetActive(false);
        //}
        //else if (health == 6)
        //{
        //    heart[6].gameObject.SetActive(false);
        //}
        //else if (health == 5)
        //{
        //    heart[5].gameObject.SetActive(false);
        //}
        //else if (health == 4)
        //{
        //    heart[4].gameObject.SetActive(false);
        //}
        //else if (health == 3)
        //{
        //    heart[3].gameObject.SetActive(false);
        //}
        //else if (health == 2)
        //{
        //    heart[2].gameObject.SetActive(false);
        //}
        //else if (health == 1)
        //{
        //    heart[1].gameObject.SetActive(false);
        //}
        //else if (health == 0)
        //{
        //    heart[0].gameObject.SetActive(false);
        //    gameOver.gameObject.SetActive(true);
        //    Time.timeScale = 0f;
        //}

        heartText.text = "X " + health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    //private void damageOverTime()
    //{
    //    if (stillCollide == true)
    //    {
    //        minusHealth();
    //        Invoke("damageOverTime", 1);
    //    }

    //}

    public void minusHealth()
    {
        health -= 1;
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if ((whatDamagePlayer.value & (1 << collision.gameObject.layer)) > 0)
    //    {
    //        minusHealth();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        stillCollide = false;
    //    }
    //}
}
