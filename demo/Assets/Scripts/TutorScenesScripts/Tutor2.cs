using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_IOS
public class Tutor2 : MonoBehaviour
{
    #region Public Fields
    public Button toggleButton;
    #endregion

     AudioSource audioSource;
    LoadScene loadScene;
    private int buttonPressCount;  // 按钮按下的计数器

    void Start()
    {

        loadScene = GetComponent<LoadScene>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(3.0f);
        buttonPressCount = 0;

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogWarning("ToggleButton is not assigned.");
        }
    }

    void OnButtonPressed()
    {
        buttonPressCount++;

        if (buttonPressCount == 2)
        {
            TriggerEvent();
            buttonPressCount = 0;  // 重置计数器
        }
    }

    void TriggerEvent()
    {
        Debug.Log("Button pressed four times! Event triggered.");
        StartCoroutine(WaitForFiveSecNextLv());
    }

    IEnumerator WaitForFiveSecNextLv()
    {
        yield return new WaitForSeconds(2.0f);
        UAP_AccessibilityManager.EnableAccessibility(true);
        UAP_AccessibilityManager.Say("You have complete this Tutorial! Jump to next level...", true, true, UAP_AudioQueue.EInterrupt.Elements);
        yield return new WaitForSeconds(5.0f);
        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");
        loadScene.LoadTheSceneSix();
    }
}


#endif