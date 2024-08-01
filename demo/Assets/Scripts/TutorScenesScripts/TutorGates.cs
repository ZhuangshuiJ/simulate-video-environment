using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class TutorGates : MonoBehaviour
{
    public GameObject gate1;

    public GameObject gate2;

    public GameObject gate3;


    Tutor1 tutor1;

    void Start()
    {
        tutor1 = GameObject.Find("SceneManager").GetComponent<Tutor1>();
        Debug.Log("TutorGates script started.");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug log to output the name of the colliding object
        Debug.Log($"{gameObject.name} collided with {other.gameObject.name}.");

        // Check if the colliding object's name is "Head"
        if (other.gameObject.name == "Head")
        {
            Debug.Log($"{gameObject.name} triggered by Head.");

            if (gameObject.name == "Gate1")
            {
                Debug.Log("Gate1 triggered.");
                StartCoroutine(DeactivateAndActivate(gate2));
            }
            else if (gameObject.name == "Gate2")
            {
                Debug.Log("Gate2 triggered.");
                StartCoroutine(DeactivateAndActivate(gate3));
            }
            else if (gameObject.name == "Gate3")
            {
                Debug.Log("Gate3 triggered.");
                StartCoroutine(DeactivateAndTriggerEvent());
            }
        }
    }

    private IEnumerator DeactivateAndActivate(GameObject nextGate)
    {
        // Deactivate this game object 's audioSource
        gameObject.GetComponent<AudioSource>().Stop();

        UAP_AccessibilityManager.EnableAccessibility(true);
        Debug.Log("UAP Enabled");
        UAP_AccessibilityManager.Say("You have crossed this gate successfully, next gate will be generate...", true, true, UAP_AudioQueue.EInterrupt.Elements);
        // Debug log before waiting
        Debug.Log("Waiting for 5 seconds.");

        // Wait for 3 seconds
        yield return new WaitForSeconds(5.0f);

        // Debug log after waiting
        Debug.Log("Waited for 5 seconds.");

        UAP_AccessibilityManager.EnableAccessibility(false);
        Debug.Log("UAP Disabled");

        Debug.Log($"Deactivating {gameObject.name}.");
        gameObject.SetActive(false);

        // Activate the next gate
        if (nextGate != null)
        {
            Debug.Log($"Activating {nextGate.name}.");
            nextGate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Next gate is null.");
        }
    }

    private IEnumerator DeactivateAndTriggerEvent()
    {
        // Deactivate this game object's audio Source
        gameObject.GetComponent<AudioSource>().Stop();


        // Trigger the event
        Debug.Log("Triggering event.");


        tutor1.TriggerEvent();
        // Debug log before waiting
        Debug.Log("Waiting for 3 seconds.");


        // Wait for 3 seconds
        yield return new WaitForSeconds(3.0f);

        // Debug log after waiting
        Debug.Log("Waited for 3 seconds.");
        Debug.Log($"Deactivating {gameObject.name}.");
        gameObject.SetActive(false);

 
    }
}
#endif