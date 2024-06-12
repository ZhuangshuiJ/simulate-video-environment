using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;

public class SyncCharacterMovement : MonoBehaviour
{
    public GameObject character; 
    public XROrigin xrOrigin; 

    void Update()
    {
        
        Vector3 devicePosition = xrOrigin.Camera.transform.position;
        Quaternion deviceRotation = xrOrigin.Camera.transform.rotation;

        
        character.transform.position = devicePosition;
        character.transform.rotation = deviceRotation;
    }
}
