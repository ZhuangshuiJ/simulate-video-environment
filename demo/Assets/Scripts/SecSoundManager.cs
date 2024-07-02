using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecSoundManager : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] AudioClip[] audioClips;
    #endregion

    #region Public Fields
    public AudioSource audioSource;
    #endregion

    #region private Fields
    private Dictionary<string, int> nameToClipIndices;
    #endregion
    void Start()
    {
        Setup();
    }
    #region Private Fields
    private void Setup()
    {
        nameToClipIndices = new Dictionary<string, int>
        {
            { "Pieta (Michelangelo)", 0 },
            { "Venus de Milo", 1 },
            { "Starry night (Van gogh)", 2 },
            { "Last Supper (Di Vinci)", 3 },
            { "The elephants (Dali)", 4 },
            { "Bridge over a pond of water lilies (Monet)", 5 },
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedObject = other.gameObject;

        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned.");
            return;
        }


        if (nameToClipIndices.TryGetValue(collidedObject.name, out int audioIndex))
        {
            
            if (audioIndex >= 0 && audioIndex < audioClips.Length)
            {
                audioSource.clip = audioClips[audioIndex];
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"Audio index {audioIndex} is out of range.");
            }
        }
        else
        {
            Debug.LogWarning($"No audio clip found for object {collidedObject.name}.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioSource.Stop();
    }
    #endregion
}
