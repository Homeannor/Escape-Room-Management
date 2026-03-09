using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public float startMoveSpeed = 30f;
    private float moveSpeed;
    public float rotationSpeed = 50f;
    public float ScrollSpeed;
    public bool panelOpen;

    public Camera topDownCamera;
    public Camera isometricCamera;

    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        /*Debug.Log("Speed: " + moveSpeed);
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        UnityEngine.Vector3 move = new UnityEngine.Vector3(moveX, 0, moveZ);
        transform.position += move * Time.deltaTime * moveSpeed;*/


        /*float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        topDownCamera.transform.Rotate(0, horizontal, 0, Space.World);
        isometricCamera.transform.Rotate(0, horizontal, 0, Space.World);*/

        float movement = moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = startMoveSpeed * 2;
        }
        else
        {
            moveSpeed = startMoveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(movement, 0, 0, Space.Self);

            /*topDownCamera.transform.Translate(movement, 0, 0, Space.World);
            isometricCamera.transform.Translate(movement, 0, 0, Space.World);*/
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movement, 0, 0, Space.Self);

            /*topDownCamera.transform.Translate(-movement, 0, 0, Space.Self);
            isometricCamera.transform.Translate(-movement, 0, 0, Space.Self);*/
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, movement, Space.Self);

            /*topDownCamera.transform.Translate(0, 0, movement, Space.Self);
            isometricCamera.transform.Translate(0, 0, movement, Space.Self);*/
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -movement, Space.Self);

            /*topDownCamera.transform.Translate(0, 0, -movement, Space.Self);
            isometricCamera.transform.Translate(0, 0, -movement, Space.Self);*/
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);

            /*topDownCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);*/
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.Self);

            /*topDownCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);*/
        }

        isometricCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        topDownCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

        if (isometricCamera.fieldOfView < 30 || topDownCamera.fieldOfView < 30)
        {
            topDownCamera.fieldOfView = 30;
            isometricCamera.fieldOfView = 30;
        }
        else if (isometricCamera.fieldOfView > 100 || topDownCamera.fieldOfView > 100)
        {
            topDownCamera.fieldOfView = 100;
            isometricCamera.fieldOfView = 100;
        }

        if (panelOpen == true)
        {
            ScrollSpeed = 0f;
        }
        else
        {
            ScrollSpeed = 20f;
        }

    }
}
 