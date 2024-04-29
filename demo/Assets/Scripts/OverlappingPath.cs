using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlappingPath : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] Transform[] Points;

    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = Points[0].transform.position;
        StartCoroutine(Step1());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Step1()
    {
        yield return new WaitForSeconds(25.0f);
        Vector3 targetPosition = Points[1].transform.position;
        moveSpeed = 3;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;// skip for 1 frame
        }
    }

    
}
