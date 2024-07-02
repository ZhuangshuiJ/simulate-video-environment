using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    public GameObject gripPoint;
    #region Private Fields
    
    Vector3 arCamLastRot;               // rotation of ARCamera in the last frame

    
    Vector3 arCamCurrRot;               // rotation of ARCamera in the this frame

    
    Vector3 arCamRotDiff;               // difference between AR Camera's last vs. current rotation
    #endregion
    void Start()
    {
        //Setup gyroscope true  
        Input.gyro.enabled = true;
    }

    
    void Update()
    {
        arCamCurrRot = Input.gyro.attitude.eulerAngles;
        arCamRotDiff = arCamCurrRot - arCamLastRot;
        arCamRotDiff = new Vector3(-arCamRotDiff.x, -arCamRotDiff.z, arCamRotDiff.y);
        gripPoint.transform.localEulerAngles += Vector3.Scale(arCamRotDiff, new Vector3(1, 1, 1));
        arCamLastRot = arCamCurrRot;
    }
}
