using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class Tutor3 : MonoBehaviour
{
    AudioSource audioSource;
    GameObject tutorArtifact;
    AudioSource artifactAudioSource;
    LoadScene loadScene;
    private bool atfAudioSourceFinded = false;
    private bool isCoroutineRunning = false; // 标志位，确保协程只运行一次

    void Start()
    {

        loadScene = GetComponent<LoadScene>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(3.0f);
    }

    void Update()
    {
        // 检查并找到目标物体
        if (!atfAudioSourceFinded)
        {
            tutorArtifact = GameObject.Find("TutorialTarget(Clone)");
            if (tutorArtifact != null)
            {
                Debug.Log("TutorialTarget Finded!");
                artifactAudioSource = tutorArtifact.GetComponent<AudioSource>();
                if (artifactAudioSource != null)
                {
                    atfAudioSourceFinded = true; // 标记目标物体已找到
                }
            }
        }

        // 检查并启动协程
        if (artifactAudioSource != null && artifactAudioSource.isPlaying && !isCoroutineRunning)
        {
            StartCoroutine(WaitAndTriggerEvent());
            isCoroutineRunning = true; // 标记协程正在运行
        }
    }

    IEnumerator WaitAndTriggerEvent()
    {
        // 等待5秒
        yield return new WaitForSeconds(5.0f);

        // 检查artifactAudioSource是否仍在播放
        if (artifactAudioSource.isPlaying)
        {
            artifactAudioSource.Stop();
            TriggerEvent();
        }

        isCoroutineRunning = false; // 协程完成，允许再次启动
    }

    void TriggerEvent()
    {
        Debug.Log("AudioSource has been playing for 5 seconds! Event triggered.");
        StartCoroutine(WaitForFiveSecNextLv());
    }

    IEnumerator WaitForFiveSecNextLv()
    {
        UAP_AccessibilityManager.EnableAccessibility(true);
        UAP_AccessibilityManager.Say("You have complete this Tutorial! Jump to next level...", true, true, UAP_AudioQueue.EInterrupt.Elements);
        yield return new WaitForSeconds(5.0f);
        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");
        loadScene.LoadTheSceneSeven();
    }
}
#endif