using System.Collections.Generic;
using UnityEngine;

public class BubbleCollision : MonoBehaviour
{
    private static readonly HashSet<string> ExcludedNames = new HashSet<string> { "Ground", "SecDetector", "Detector","GripPoint" ,"Gate1", "Gate2", "Gate3"};

    private void OnTriggerEnter(Collider other)
    {
        if (!ExcludedNames.Contains(other.gameObject.name))
        {
            // 调用震动函数
            Handheld.Vibrate();
            Debug.Log("碰到 " + other.gameObject.name + "，触发震动");
        }
    }
}
