using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public TMP_Text heartText;
    public string sceneName;

    public static PlayerHealth instance;

    // Update is called once per frame
    void Update()
    {
        heartText.text = "X " + health.ToString();

        if (health <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void minusHealth()
    {
        health -= 1;
    }

}
