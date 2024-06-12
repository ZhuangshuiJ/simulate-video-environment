using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HearXR;
using Unity.XR.CoreUtils;




 public class PlayerMovement_IOS : MonoBehaviour
 {
    #if UNITY_IOS

    #region Private Fields
    
    XROrigin xrOrigin;
    bool _motionAvailable;
    bool _headphoneConnected;
    GameObject head;
    GameObject player;
    #endregion

    #region Public Fields
    public GameObject Camera;
    public GameObject body;
    public float rotSpeed = 70f;
    public TMP_Text _motionAvailabilityText = default;
    public TMP_Text _headphoneConnectionStatusText = default;
    #endregion
    private void Start()
    {
        // This call initializes the native plugin.
        HeadphoneMotion.Init();
        // Object initialize
        ObjSetup();
        
        

        _motionAvailable = HeadphoneMotion.IsHeadphoneMotionAvailable();
        _motionAvailabilityText.text =
                (_motionAvailable) ? "Headphone motion is available" : "Headphone motion is not available";
        if (_motionAvailable)
        {

            // Set headphones connected text to false to start with.
            HandleHeadphoneConnectionChange(false);

            // Subscribe to events before starting tracking, or will miss the initial headphones connected callback.
            // Subscribe to the headphones connected/disconnected event.
            HeadphoneMotion.OnHeadphoneConnectionChanged += HandleHeadphoneConnectionChange;

            // Subscribe to the rotation callback.
            // Alternatively, you can subscribe to OnHeadRotationRaw event to get the 
            // x, y, z, w values as they come from the API.
            HeadphoneMotion.OnHeadRotationQuaternion += HandleHeadRotationQuaternion;

            // Start tracking headphone motion.
            HeadphoneMotion.StartTracking();
        }
       
    }

    private void LateUpdate()
    {
        if(xrOrigin != null)
        {
            player.transform.position = xrOrigin.Camera.transform.position;
            
        }

        if(xrOrigin != null)
        {
            Camera.transform.rotation = head.transform.rotation;
        }
    }

   

    #region Private Method
    //apply the headphone's rotation to player's head rotation
    private void HandleHeadRotationQuaternion(Quaternion rotation)
    {
        head.transform.rotation = rotation;
        
    }

   
    // initialize object
    
    private void ObjSetup()
    {
        //assign head and player gameobject
         head = transform.Find("Head").gameObject;
         player = GameObject.Find("Player").gameObject;

        //assign XROrigin object
        GameObject xrOriginobj = GameObject.Find("XR Origin");
        
        //if we get the xrorigin object successfull, assign the XROrigin component to our xrOrigin
        if (xrOriginobj != null)
        {
            xrOrigin = xrOriginobj.GetComponent<XROrigin>();
        }
        else
        {
            Debug.LogError("XR Origin object not found");
        }
 

        if(head == null)
        {
            Debug.LogError("Head object not found");
        }

        if(player == null)
        {
            Debug.LogError("Player object not found");
        }
    }

    private void HandleHeadphoneConnectionChange(bool connected)
    {
        _headphoneConnected = connected;
        _headphoneConnectionStatusText.text =
               (_headphoneConnected) ? "Headphones are connected" : "Headphones are not connected";
    }

    private void OnDestroy()
    {
        if(_motionAvailable)
        {
            HeadphoneMotion.OnHeadphoneConnectionChanged -= HandleHeadphoneConnectionChange;
            HeadphoneMotion.OnHeadRotationQuaternion -= HandleHeadRotationQuaternion;
            HeadphoneMotion.StopTracking();
        }
    }
    #endregion
#endif
}