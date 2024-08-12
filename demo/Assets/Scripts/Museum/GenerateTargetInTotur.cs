using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
public class GenerateTargetInTotur : MonoBehaviour
{
    #region Editor Fields
    [SerializeField] private GameObject targetPrefab;
    #endregion

    #region Private Fields
    private float randXaxis;
    private float randZaxis;
    #endregion

    void Start()
    {
        GenerateTarget();
    }

    void Update()
    {
        // You can call GenerateTarget() here if needed

    }

    #region Private Methods

    private void GenerateTarget()
    {
        if (targetPrefab != null)
        {
            randXaxis = GetBoundaryRandomValue();
            randZaxis = GetBoundaryRandomValue();

            Vector3 randomPosition = new Vector3(randXaxis, 4f, randZaxis);
            Instantiate(targetPrefab, randomPosition, Quaternion.identity);
            Debug.Log("Target Prefab instantiated at position: " + randomPosition);
        }
        else
        {
            Debug.LogError("Target Prefab is not assigned.");
        }
    }

    private float GetBoundaryRandomValue()
    {
        // 50% chance to pick a value in either the negative or positive boundary
        if (Random.value < 0.5f)
        {
            return Random.Range(-15f, -10f);
        }
        else
        {
            return Random.Range(10f, 15f);
        }
    }

    #endregion
}
#endif