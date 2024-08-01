using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_IOS
public class SecDetectorInTutor4 : MonoBehaviour
{
    int currentSceneIndex;
    private float stayTime = 0f; // 用于记录触发器的停留时间
    private bool eventTriggered = false; // 确保事件只触发一次
    LoadScene loadScene;

    // Start is called before the first frame update
    void Start()
    {
        loadScene = GameObject.Find("SceneManager").GetComponent<LoadScene>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (currentSceneIndex == 6)
        {
            Debug.Log("111111111");
            stayTime += Time.deltaTime; // 增加停留时间
            Debug.Log("Stay Time: " + stayTime); // 调试日志

            if (!eventTriggered && stayTime >= 7.0f)
            {
                eventTriggered = true; // 确保事件只触发一次
                TriggerEvent();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentSceneIndex == 6)
        {
            stayTime = 0f; // 当对象离开触发器时重置停留时间
            eventTriggered = false; // 重置事件触发状态
        }
    }

    void TriggerEvent()
    {
        Debug.Log("SecDetector has been in the trigger for 7 seconds! Event triggered.");
        // 在这里添加你想要触发的事件逻辑
        StartCoroutine(WaitForFiveSecNextLv());
    }

    IEnumerator WaitForFiveSecNextLv()
    {
        UAP_AccessibilityManager.EnableAccessibility(true);
        UAP_AccessibilityManager.Say("You have complete all Tutorial so far!!!    Back to mainmenu...", true, true, UAP_AudioQueue.EInterrupt.Elements);
        yield return new WaitForSeconds(5.0f);
        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");
        loadScene.LoadTheFirstScene();
    }
}
#endif