using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSecondScene : MonoBehaviour
{
    private void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1) 
        {
            StartCoroutine(MainMenuCount());
        }
    }
    public void LoadTheFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTheSecondScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator MainMenuCount()
    {
        yield return new WaitForSeconds(232.0f);
        LoadTheFirstScene();
    }
}
