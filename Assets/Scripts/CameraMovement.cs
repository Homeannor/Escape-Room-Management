using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;

    public Camera topDownCamera;
    public Camera isometricCamera;

    void Update()
    {
        /*float moveX = Input.GetAxis("Horizontal");
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
            moveSpeed = 20f;
        }
        else
        {
            moveSpeed = 10f;
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
    }
}
 