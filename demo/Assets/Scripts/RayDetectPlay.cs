using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDetectPlay : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Transform headTransform; // 人物眼睛的Transform
    [SerializeField] float maxDistance = 100f; // 射线最大距离
    #endregion


    #region Private Fields
    private AudioSource audioSource;
    private Dictionary<string, int> tagToClipIndices;
    private int lastPlayedClipIndex = -1; // 用于存储最后播放的音频剪辑索引
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


    void Update()
    {
        DetectAndPlaySound();
        Debug.DrawRay(headTransform.position, headTransform.forward * maxDistance, Color.red);

    }
    #region Private Methods
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
                //Stop the current audio clip prevent from overlap
                audioSource.Stop();
                audioSource.clip = audioClips[index];
                audioSource.Play();
                Debug.Log($"Playing audio clip: {audioClips[index].name}");
                //update last played audioclip's index 
                lastPlayedClipIndex = index;
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

    private void DetectAndPlaySound()
    {
        RaycastHit[] hits;
        Ray ray = new Ray(headTransform.position, headTransform.forward);
        hits = Physics.RaycastAll(ray, maxDistance);
        if (hits.Length > 0)
        {
            RaycastHit closestHit = hits[0];
            float closestDistance = hits[0].distance;

            foreach (RaycastHit hit in hits)
            {
                if (hit.distance < closestDistance)
                {
                    closestHit = hit;
                    closestDistance = hit.distance;
                }
            }

            if (tagToClipIndices.ContainsKey(closestHit.collider.tag))
            {
                int clipIndex = tagToClipIndices[closestHit.collider.tag];
                // Only play new audioclip when the ray detected obj's index different than last played obj's audioclip(prevent from overlap)
                if (clipIndex != lastPlayedClipIndex)
                {
                    PlayAudioClip(clipIndex);
                }
                Debug.Log($"Closest object is {closestHit.collider.tag} at distance {closestDistance}");
            }
            else
            {
                //if didn't detect any corresponding obj in the dictionery tagToClipIndices,stop current playing and reset the last play audioclip index
                audioSource.Stop();
                lastPlayedClipIndex = -1;
            }
        }
    }
    #endregion
}
