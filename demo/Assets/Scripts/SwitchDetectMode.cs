using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDetectMode : MonoBehaviour
{

    #region Editor Fields
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource buttonAudioSourse;
    HeadRaycaster headRaycaster;
    #endregion

    #region Private Fields

    GameObject razer;
    GameObject head;
    GameObject detector;
    GameObject secDetector;
    GameObject gripPoint;

    SecSoundManager secSoundManager;
    SoundManager soundManager;
   
    

    bool rayMode = false;
    bool caneMode = false;
    bool firstClick = true;
    #endregion
    void Start()
    {
        ObjSetup();
    }

    void Update()
    {
        SwitchMode();
    }

#region Private Method
    private void ObjSetup()
    {
        

        head = transform.Find("Head")?.gameObject;
        if(head == null)
        {
            Debug.LogError("Head object not found");
            return;
        }

        razer = head.transform.Find("Razer")?.gameObject;
        if (razer == null)
        {
            Debug.LogError("Ray object not found");
            return;
        }

       
        gripPoint = GameObject.Find("Player/Head/GripPoint");
        if (gripPoint == null)
        {
            Debug.LogError("GripPoint object not found");
            return;
        }

        detector = GameObject.Find("Player/Head/GripPoint/Cane/Detector");
        if (detector == null)
        {
            Debug.LogError("Detector object not found");
            return;
        }

        secDetector = GameObject.Find("Player/Head/GripPoint/Cane/SecDetector");
        if (secDetector == null)
        {
            Debug.LogError("SecDetector object not found");
            return;
        }
      
        if (head != null)
        {
            headRaycaster = head.GetComponent<HeadRaycaster>();
            if (headRaycaster == null)
            {
                Debug.LogError("HeadRaycaster component not found on Head object");
                return;
            }

            soundManager = detector.GetComponent<SoundManager>();
            if (soundManager == null)
            {
                Debug.LogError("SoundManager component not found on Detector object");
                return;
            }

            secSoundManager = secDetector.GetComponent<SecSoundManager>();
            if (secSoundManager == null)
            {
                Debug.LogError("SecSoundManager component not found on Detector object");
                return;
            }
        }
        
    }

    

    private void SwitchMode()
    {
        if (razer != null)
        {
            razer.SetActive(rayMode);
        }

        if (headRaycaster != null)
        {
            headRaycaster.enabled = rayMode;
        }

        if (soundManager != null)
        {
            soundManager.enabled = caneMode;
        }

        if (gripPoint != null)
        {
            gripPoint.SetActive(caneMode);
        }

    }
    #endregion
    #region Public method
    public void SwitchButtonOnclick()//Onclick event for button
    {
        if (firstClick)
        {
            rayMode = true;
            caneMode = false;
            firstClick = false; // After the first click, this won't run again
        }
        else
        {
            // Toggle modes after the first click
            headRaycaster.lastArtifact.StopAudio();
            soundManager.audioSource.Stop();
            secSoundManager.audioSource.Stop();
            rayMode = !rayMode;
            caneMode = !caneMode;

        }

        if (rayMode)
        {
            buttonAudioSourse.clip = audioClips[0];
            buttonAudioSourse.Play();
        }

        if (caneMode)
        {
            buttonAudioSourse.clip = audioClips[1];
            buttonAudioSourse.Play();
        }

    }
    #endregion
}
