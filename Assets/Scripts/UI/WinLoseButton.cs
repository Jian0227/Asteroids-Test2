using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseButton : MonoBehaviour
{

    public void startLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
