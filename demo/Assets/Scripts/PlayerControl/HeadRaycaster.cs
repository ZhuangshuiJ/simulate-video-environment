using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRaycaster : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] Transform casterTransform;
    [SerializeField] float maxDistance = 100f;

    public Artifact lastArtifact = null;
    private string lastArtifactTag = "";

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(casterTransform.position, casterTransform.up);
        Debug.DrawRay(casterTransform.position, casterTransform.up * maxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            Artifact artifact = hit.collider.GetComponent<Artifact>();

            if (artifact != null)
            {
                string currentTag = hit.collider.tag;


                if (artifact != lastArtifact || currentTag != lastArtifactTag)
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