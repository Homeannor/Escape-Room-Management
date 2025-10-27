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

        if (Input.GetKey(KeyCode.D))
        {
            topDownCamera.transform.Translate(movement, 0, 0, Space.World);
            isometricCamera.transform.Translate(movement, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            topDownCamera.transform.Translate(-movement, 0, 0, Space.World);
            isometricCamera.transform.Translate(-movement, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.W))
        {
            topDownCamera.transform.Translate(0, 0, movement, Space.World);
            isometricCamera.transform.Translate(0, 0, movement, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            topDownCamera.transform.Translate(0, 0, -movement, Space.World);
            isometricCamera.transform.Translate(0, 0, -movement, Space.World);
        }

        if (Input.GetKey(KeyCode.E))
        {
            topDownCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            topDownCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}
 