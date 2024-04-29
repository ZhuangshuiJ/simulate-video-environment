using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public bool lockTranslation = false;              // an boolean variable allow disabling avatar's movement (rotation still available)

    GameObject player;
    GameObject head;                                  // will rotate head use keyboard to test spatial sounds in editor

    Vector3 userRot;                                  // For dynamically recording local euler angle (rotation) of user obj (Mouse movement will continuously update its rotation)


    public float moveSpeed = 10f;       // speed of user's translation
    public float rotSpeed = 70f;        // speed of user's rotation
    float updatedMoveSpeed;             // for enabling slow down movement
    float updatedrotSpeed;

    public float sensitivity = 10f;     // control the sensitivity of mouse in the game
    
    void Start()
    {
        MovementSetup();
    }

    
    void Update()
    {
        PositionControl();
        RotationControl();
    }

    void MovementSetup()
    {
        player = GameObject.Find("Player").gameObject;
        /* Assigning "Head" object */

        head = transform.Find("Head").gameObject;

        /* Initialize these Vector 3 with the starting local euler angle (an representation of rotation) in the scene */
        userRot = transform.localEulerAngles;

        /* Lock the mouse into game once start */
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Method for controlling user's position when running on Unity Editor
    /// </summary>
    void PositionControl()
    {
        /* Update avatar's movement only when movement is not locked */
        if(!lockTranslation)
        {
            /* Press "Shift" to slow down the move speed when needed */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                updatedMoveSpeed = moveSpeed * 0.1f;
                updatedrotSpeed = rotSpeed * 0.5f;
            }
            else
            {
                updatedMoveSpeed = moveSpeed;
                updatedrotSpeed = rotSpeed;
            }

            /* Using WASD to control User body's translation */
            if (Input.GetKey(KeyCode.W))
            {
                player.transform.Translate(Vector3.forward * updatedMoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                player.transform.Translate(Vector3.back * updatedMoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                player.transform.Translate(Vector3.left * updatedMoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                player.transform.Translate(Vector3.right * updatedMoveSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// Method for controlling user's rotation when running on Unity Editor
    /// </summary>
    void RotationControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        player.transform.Rotate(Vector3.up, mouseX);

        head.transform.Rotate(Vector3.right, -mouseY);
    }

   
}

