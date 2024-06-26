using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRaycaster : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Transform headTransform;
    [SerializeField] float maxDistance = 100f;

    private Artifact lastArtifact = null;
    private string lastArtifactTag = "";

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(headTransform.position, headTransform.forward);
        Debug.DrawRay(headTransform.position, headTransform.forward * maxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Artifact artifact = hit.collider.GetComponent<Artifact>();

            if (artifact != null)
            {
                string currentTag = hit.collider.tag;

                if (artifact != lastArtifact)
                {
                    if (lastArtifact != null)
                    {
                        lastArtifact.StopAudio();
                    }

                    artifact.PlayAudio(audioClips);
                    lastArtifact = artifact;
                    lastArtifactTag = currentTag;
                }
            }
        }
        else
        {
            if (lastArtifact != null)
            {
                lastArtifact.StopAudio();
                lastArtifact = null;
                lastArtifactTag = "";
            }
        }
    }
}