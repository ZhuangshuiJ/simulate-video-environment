using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HearXR;
using Unity.XR.CoreUtils;



#if UNITY_IOS
 public class PlayerMovement_IOS_touch : MonoBehaviour
 {
    

    #region Private Fields
    
    private bool _motionAvailable;
    private bool _headphoneConnected;
    private GameObject head;
    private GameObject player;
    private AudioSource audioSource;

    private bool isTouching = false;
    private float curTime = 0;

    private bool isShaking = false;

    private Vector3 lastPosition;
    private float distanceMoved = 0.0f;
    
    private Vector3 previousAcceleration;
    private Vector3 currentAcceleration;
    #endregion

    #region Public Fields
    public GameObject body;
    public TMP_Text _motionAvailabilityText = default;
    public TMP_Text _headphoneConnectionStatusText = default;
    public float moveSpd;
    public float shakeThreshold = 2.0f;
    public float shakeSpeed = 5.0f;

    public float footStepDistanceThreshold = 1.0f;
    public AudioClip footstepSound;
    
    #endregion
    private void Start()
    {
        // This call initializes the native plugin.
        HeadphoneMotion.Init();
        previousAcceleration = Input.acceleration;
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

    private void Update()
    {
        MovementControl();   
    }

   

    #region Private Method
    //apply the headphone's rotation to player's head rotation
    private void HandleHeadRotationQuaternion(Quaternion rotation)
    {
        if(head != null)
        {
             head.transform.rotation = rotation;
        }
    }

   
    // initialize object
    
    private void ObjSetup()
    {
        lastPosition = transform.position;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = footstepSound;
        //assign head and player gameobject
        head = transform.Find("Head")?.gameObject;
         player = GameObject.Find("Player")?.gameObject;
        
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

    void MovementControl()
    {
        OnTouchHandle();
        OnShakeHandle();
        PositionControl();
        PlayFootstepSound();
    }

     private void OnTouchHandle()
    {
        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            // Get the first touch
            Touch touch = Input.GetTouch(0);

            curTime += Time.deltaTime;
            if (curTime < 2.0f)
            {
                return;
            }

            // Check the phase of the touch
            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                isTouching = true;
                moveSpd = 1.0f;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
                curTime = 0;
            }
        }
        else
        {
            isTouching = false;
            curTime = 0;
        }
    }

    private void OnShakeHandle()
        {
            currentAcceleration = Input.acceleration;
            //calculate the acceleration change
            Vector3 deltaAcceleration = currentAcceleration - previousAcceleration;

            if (deltaAcceleration.magnitude > shakeThreshold)
            {
                isShaking = true;
                moveSpd = 5.0f;
            }
            else
            {
                isShaking = false;
            }
            previousAcceleration = currentAcceleration;
        }

    private void PositionControl()
        {
            if (isTouching || isShaking)
            {
                Vector3 forward = head.transform.forward;
                Vector3 moveStep = Vector3.Scale(forward * moveSpd * Time.deltaTime, new Vector3(1, 0, 1));
                player.transform.position += moveStep;
            }

            Vector3 curRotation = Vector3.Scale(head.transform.eulerAngles, new Vector3(0, 1, 0));
            body.transform.eulerAngles = curRotation;
        }

    private void PlayFootstepSound()
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        distanceMoved += distance;
        if (distanceMoved >= footStepDistanceThreshold)
        {
            if (audioSource != null && footstepSound != null)
            {
                audioSource.Play();
            }
            distanceMoved = 0.0f; 
        }
        lastPosition = transform.position;
    }
        #endregion

}

#endif