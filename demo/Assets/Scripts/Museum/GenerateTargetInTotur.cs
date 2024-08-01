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
    private Vector3 centerPosition = new Vector3(14f, 4f, 14f);
    private float range = 15f;
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
            randXaxis = Random.Range(-range, range);
            randZaxis = Random.Range(-range, range);

            Vector3 randomPosition = new Vector3(randXaxis, centerPosition.y, randZaxis);
            Instantiate(targetPrefab, randomPosition, Quaternion.identity);
            Debug.Log("Target Prefab instantiated.");
        }
        else
        {
            Debug.LogError("Target Prefab is not assigned.");
        }
    }

    #endregion
}
#endif