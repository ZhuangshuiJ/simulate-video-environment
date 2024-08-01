using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_IOS
public class LoadScene : MonoBehaviour
{

    int currentSceneIndex;
    float tapTimeWindow;
    int tapCount;
    float doubleTapTime = 0.3f;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        UAP_AccessibilityManager.EnableAccessibility(true);
        Debug.Log("UAP Enabled");

        StartCoroutine(WelcomeToSceneWithDelay(currentSceneIndex, 0.5f)); // 延迟0.5秒
    }

    private void Update()
    {
        DoubleTapToMainMenu();
    }

    public void LoadTheFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadTheSecondScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTheThirdScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTheSceneFour()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadTheSceneFive()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadTheSceneSix()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadTheSceneSeven()
    {
        SceneManager.LoadScene(6);
    }
    public void QuitApp()
    {
        Application.Quit();
    }

    private void DoubleTapToMainMenu()
    {
        if (currentSceneIndex != 0)
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    tapCount++;

                    if (tapCount == 1)
                    {
                        tapTimeWindow = Time.time;
                    }
                    else if (tapCount == 2 && Time.time - tapTimeWindow < doubleTapTime)
                    {
                        SceneManager.LoadScene(0);
                        tapCount = 0;
                    }
                }

                if (Time.time - tapTimeWindow > doubleTapTime)
                {
                    tapCount = 0;
                }
            }
        }
    }

    private IEnumerator WelcomeToSceneWithDelay(int currentSceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay); // 延迟指定时间
        //Handheld.Vibrate();
        //Debug.Log("Viabrate!!!");
        switch (currentSceneIndex)
        {
            case 0:
                UAP_AccessibilityManager.Say("Welcome to Main menu", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Main Menu");
                break;
            case 1:
                UAP_AccessibilityManager.Say("Welcome to Tutor menu", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Tutor Menu");
                break;
            case 2:
                UAP_AccessibilityManager.Say("Welcome to Virtual Museum", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Virtual Museum");
                StartCoroutine(WaitForTwoSecCloseUAP());
                break;
            case 3:
                UAP_AccessibilityManager.Say("Welcome to Tutor1", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Tutor1");
                StartCoroutine(WaitForTwoSecCloseUAP());
                break;
            case 4:
                UAP_AccessibilityManager.Say("Welcome to Tutor2", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Tutor2");
                StartCoroutine(WaitForTwoSecCloseUAP());
                break;
            case 5:
                UAP_AccessibilityManager.Say("Welcome to Tutor3", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Tutor3");
                StartCoroutine(WaitForTwoSecCloseUAP());
                break;
            case 6:
                UAP_AccessibilityManager.Say("Welcome to Tutor4", true, true, UAP_AudioQueue.EInterrupt.Elements);
                Debug.Log("Welcome to Tutor4");
                StartCoroutine(WaitForTwoSecCloseUAP());
                break;
        }
    }

    IEnumerator WaitForTwoSecCloseUAP()
    {
        yield return new WaitForSeconds(3.0f);
        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");
    }
}
#endif