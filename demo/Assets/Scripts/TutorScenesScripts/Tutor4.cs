using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class Tutor4 : MonoBehaviour
{
    
    AudioSource audioSource;

    

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(3.0f);

 
    }       
 }
#endif

