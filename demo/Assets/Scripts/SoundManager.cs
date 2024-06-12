using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] private float movementThreshold = 0.1f; //to detect the cane is moving or not
    #endregion

    #region private Fields
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private bool isPlaying;
    #endregion
    void Start()
    {
        setUp();
    }

    void setUp()
    {
        audioSource = GetComponent<AudioSource>();

        if(audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }

        lastPosition = transform.position;
    }

    #region Private Method
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WoodFloor"))
        {
            Debug.Log("Object Detected!");
            PlayAudioClip(0);
        }
       
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("WoodFloor"))
    //    {
    //        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
    //        lastPosition = transform.position;

    //        if (speed > movementThreshold && !isPlaying)
    //        {
                
    //            PlayAudioClip(1);
                
    //            isPlaying = true;
    //        }
    //        else if (speed <= movementThreshold && isPlaying)
    //        {
                
    //            audioSource.Stop();
    //            isPlaying = false;
    //        }
    //    }
    //}

    private void PlayAudioClip(int index)
    {
        if(audioClips.Length > 0)
        {
            if (index < audioClips.Length)
            {
                audioSource.clip = audioClips[index];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioClip index is out of bounds.");
            }
        }
        else
        {
            Debug.LogWarning("AudioClips array is empty.");
        }
    }
    #endregion
}
