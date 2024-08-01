using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class Tutor1 : MonoBehaviour
{
    private GameObject player;
    private GameObject Gate1;
    private GameObject Gate2;
    private GameObject Gate3;

    AudioSource audioSource;
    LoadScene loadScene;
    void Start()
    {
        ObjSetup();
        StartCoroutine(ActivateGate1AfterDelay(20.0f));

    }

    void Update()
    {
        
    }

    void ObjSetup()
    {
        loadScene = GetComponent<LoadScene>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(3.0f);
        player = GameObject.Find("Player");
        Gate1 = GameObject.Find("Gate1");
        Gate2 = GameObject.Find("Gate2");
        Gate3 = GameObject.Find("Gate3");

        // Make sure gates are initially inactive
        if (Gate1 != null) Gate1.SetActive(false);
        if (Gate2 != null) Gate2.SetActive(false);
        if (Gate3 != null) Gate3.SetActive(false);

        if (player == null) Debug.Log("player null");
        if (Gate1 == null) Debug.Log("Gate1 null");
        if (Gate2 == null) Debug.Log("Gate2 null");
        if (Gate3 == null) Debug.Log("Gate3 null");
    }

    IEnumerator ActivateGate1AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Gate1 != null)
        {
            Gate1.SetActive(true);
            Debug.Log("Gate1 has been activated.");
        }
    }

    public void TriggerEvent()
    {
        Debug.Log("角色已经完成所有目标，触发事件！");
        StartCoroutine(WaitForFiveSecNextLv());

        // 这里是你希望在移动一定距离后触发的事件
        
    }

    IEnumerator WaitForFiveSecNextLv()
    {
        UAP_AccessibilityManager.EnableAccessibility(true);
        UAP_AccessibilityManager.Say("You have complete this Tutorial! Jump to next level...", true, true, UAP_AudioQueue.EInterrupt.Elements);
        yield return new WaitForSeconds(5.0f);
        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");
        loadScene.LoadTheSceneFive();
    }
}


#endif