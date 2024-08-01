using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class GyroControl : MonoBehaviour
{
    #region Public Fields
    public GameObject gripPoint;
    public GameObject body;
    #endregion

    #region Private Fields

    Vector3 arCamLastRot;               // rotation of ARCamera in the last frame
    Vector3 arCamCurrRot;               // rotation of ARCamera in the this frame
    Vector3 arCamRotDiff;               // difference between AR Camera's last vs. current rotation
    Quaternion initialRotation;         // cane's initial rotation

    GameObject shadowGripPoint;
    #endregion

    void Start()
    {
        // Enable gyroscope
        Input.gyro.enabled = true;
        shadowGripPoint = GameObject.Find("Player/Head/ShadowGripPoint");
    }

    public void initialCaneRotation()
    {
        gripPoint.transform.localRotation = Quaternion.identity;
        initialRotation = gripPoint.transform.localRotation;

        //arCamLastRot = Input.gyro.attitude.eulerAngles;
        Debug.Log("Cane rotation initialized");
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