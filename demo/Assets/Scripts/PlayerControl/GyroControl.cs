using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class GyroControl : MonoBehaviour
{
    #region Public Fields
    public GameObject gripPoint;
  
    public float rotationThreshold = 30f; // 30度的旋转阈值
    #endregion

    #region Private Fields
    Vector3 arCamLastRot;               // 上一帧的ARCamera旋转
    Vector3 arCamCurrRot;               // 当前帧的ARCamera旋转
    Vector3 arCamRotDiff;               // 当前帧与上一帧旋转的差值
    Quaternion initialRotation;         // 手柄的初始旋转
    GameObject shadowGripPoint;
      private GameObject head;
    #endregion

    void Start()
    {
        head = GameObject.Find("Player/Head");
        if (head == null)
        {
            Debug.Log("head is null");
        }

        // 启用陀螺仪
        Input.gyro.enabled = true;
        shadowGripPoint = GameObject.Find("Player/Head/ShadowGripPoint");
        initialCaneRotation();

       
    }

    public void initialCaneRotation()
    {
        // 重置cane旋转
        gripPoint.transform.localRotation = Quaternion.identity;

        // 设置初始旋转为头部的当前局部旋转
        initialRotation = head.transform.localRotation;
        gripPoint.transform.localRotation = initialRotation;

        // 记录当前陀螺仪旋转
        arCamLastRot = Input.gyro.attitude.eulerAngles;
        Debug.Log("cane旋转已初始化");
    }

    void Update()
    {
        gripPoint.transform.position = shadowGripPoint.transform.position;
        arCamCurrRot = Input.gyro.attitude.eulerAngles;
        arCamRotDiff = arCamCurrRot - arCamLastRot;
        arCamRotDiff = new Vector3(-arCamRotDiff.x, -arCamRotDiff.z, arCamRotDiff.y);
        gripPoint.transform.localEulerAngles += Vector3.Scale(arCamRotDiff, new Vector3(1, 1, 0));
        arCamLastRot = arCamCurrRot;
    }
}
#endif