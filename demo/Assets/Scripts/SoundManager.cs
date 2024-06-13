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
    private bool hasPlayedHitSound; // to track if the hit sound has been played
    private Dictionary<string, int[]> tagToClipIndices;
    #endregion
    void Start()
    {
        setUp();                
    }



    #region Private Method

    private void setUp()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }

        lastPosition = transform.position;

        // Initialize the dictionary to map tags to their respective audio clip indices
        tagToClipIndices = new Dictionary<string, int[]>
        {
            { "WoodFloor", new int[] { 0, 1 } },
            { "WoodWall", new int[] { 2, 3 } },
            { "Fabric", new int[] { 4, 5 } },
            { "Wood", new int[] { 6, 7 } }
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        HandleTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        HandleTriggerExit(other);
    }

    private void PlayAudioClip(int index)
    {
        if(audioClips.Length > 0)
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

    //Handle the OnTriggerEnter event
    private void HandleTriggerEnter(Collider other)
    {
        //check if the collier's tag was exist in the dictionary tagToClipIndices
        //if exist, play the hit sound.
        if (tagToClipIndices.ContainsKey(other.tag))
        {
            Debug.Log($"{other.tag} Detected on Enter!");
            PlayAudioClip(tagToClipIndices[other.tag][0]);
            StartCoroutine(WaitForHitSound(other.tag)); // start the coroutine
            
        }
    }

    //Handle the OnTriggerStay event
    private void HandleTriggerStay(Collider other)
    {
        //check if the collier's tag was exist in the dictionary tagToClipIndices
        //if exist, play the slide sound.
        if (tagToClipIndices.ContainsKey(other.tag))
        {
            Debug.Log($"{other.tag} Detected on Stay!");

            if (hasPlayedHitSound)
            {
                PlayWhileSliding(tagToClipIndices[other.tag][1]);
            }
                 
        }
    }

    //Handle the OnTriggerExit event
    private void HandleTriggerExit(Collider other)
    {
        // Stop all sounds if the collider's tag exists in the dictionary tagToClipIndices
        if (tagToClipIndices.ContainsKey(other.tag))
        {
            Debug.Log($"{other.tag} Detected on Exit!");
            audioSource.loop = false;
            audioSource.Stop();
            isPlaying = false;
            hasPlayedHitSound = false; // reset the flag
        }
    }
    private IEnumerator WaitForHitSound(string tag)
    {
        // Wait for the hit sound to finish
        yield return new WaitForSeconds(audioSource.clip.length);
        // Allow the slide sound to play
        hasPlayedHitSound = true;

    }

    //Play audio clip while the cane is sliding
    private void PlayWhileSliding(int index)
    {
        // calculates the speed of the object by determining the distance it has moved since the last frame, and store in a variable
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;

        //Update lastPosition to the current position of the object
        lastPosition = transform.position;

        //If the calculated speed is greater than the movementThreshold and no sound is currently playing 
        if (speed > movementThreshold && !isPlaying && hasPlayedHitSound)
        {
            Debug.Log("Starting slide sound.");

            audioSource.loop = true;

            PlayAudioClip(index);

            isPlaying = true;
        }

        //If the calculated speed is less than or equal to the movementThreshold and sound is currently playing
        else if (speed <= movementThreshold && isPlaying)
        {
            audioSource.loop = false;

            audioSource.Stop();

            isPlaying = false;
        }
    }
    #endregion
}
