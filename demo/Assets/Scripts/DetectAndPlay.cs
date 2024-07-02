using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAndPlay : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] AudioClip[] audioClips;
    #endregion

    #region Private Fields
    //private bool FirstObjDetected = false;
    private AudioSource audioSource;
    private Dictionary<string, int> tagToClipIndices;
    #endregion
    void Start()
    {
        Setup();
        tagToClipIndices = new Dictionary<string, int>
        {
            { "WoodFloor",0 },
            { "WoodWall",1  },
            { "Fabric",2    },
            { "Wood",3      },
            {"Frank",4      },
            {"FBI",5        }
        };
    }

    #region Private Method
    private void Setup()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    private void PlayAudioClip(int index)
    {
        if (audioClips.Length > 0)
        {
            if (index < audioClips.Length)
            {
                audioSource.clip = audioClips[index];
                audioSource.Play();
                Debug.Log($"Playing audio clip: {audioClips[index].name}");
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

    private void OnTriggerEnter(Collider other)
    {
        //FirstObjDetected = true;
        HandleTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
       // FirstObjDetected = false;
        // Stop all sounds if the collider's tag exists in the dictionary tagToClipIndices
        if (tagToClipIndices.ContainsKey(other.tag))
        {
            Debug.Log($"Razer exit {other.tag} !");
            
        }
    }
    
    private void HandleTriggerEnter(Collider other)
    {
        //check if the collier's tag was exist in the dictionary tagToClipIndices
        //if exist, play the hit sound.
        if (tagToClipIndices.ContainsKey(other.tag))
        {
            Debug.Log($"Razer detected {other.tag} !");
            PlayAudioClip(tagToClipIndices[other.tag]);
        }
    }
    #endregion
}
