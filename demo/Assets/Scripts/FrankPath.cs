using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankPath : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] Transform[] Points;

    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = Points[0].transform.position;
        StartCoroutine(Step1());
        StartCoroutine(Step2());
        StartCoroutine(Step3());
        StartCoroutine(Step4());
        StartCoroutine(Step5());
        StartCoroutine(Step6());
        StartCoroutine(Step7());
        StartCoroutine(Step8());

    }
        
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Step1()
    {
        yield return new WaitForSeconds(18.0f);
        Vector3 targetPosition = Points[1].transform.position;
        moveSpeed = 3;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;// skip for 1 frame
        }
    }

    IEnumerator Step2()
    {
        yield return new WaitForSeconds(24.0f);
        Vector3 targetPosition = Points[2].transform.position;
        moveSpeed = 5;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step3()
    {
        yield return new WaitForSeconds(75.0f);
        Vector3 targetPosition = Points[3].transform.position;
        moveSpeed = 3;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step4()
    {
        yield return new WaitForSeconds(97.0f);
        Vector3 targetPosition = Points[4].transform.position;
        moveSpeed = 3;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step5()
    {
        yield return new WaitForSeconds(110.0f);
        Vector3 targetPosition = Points[2].transform.position;
        moveSpeed = 1;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step6()
    {
        yield return new WaitForSeconds(122.0f);
        Vector3 targetPosition = Points[6].transform.position;
        moveSpeed = 2;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step7()
    {
        yield return new WaitForSeconds(138.0f);
        Vector3 targetPosition = Points[4].transform.position;
        moveSpeed = 2;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Step8()
    {
        yield return new WaitForSeconds(155.0f);
        Vector3 targetPosition = Points[5].transform.position;
        moveSpeed = 2;
        while (target.transform.position != targetPosition)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
