using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public float startMoveSpeed = 30f;
    private float moveSpeed;
    public float rotationSpeed = 50f;
    public float ScrollSpeed;
    public float lookSpeedx;
    public float lookSpeedy;
    public Transform orientation;
    float xRotation;
    float yRotation;
    public bool panelOpen;

    public Camera topDownCamera;
    public Camera isometricCamera;
    

    private void Start()
    {
        instance = this;

        Vector3 angles = transform.rotation.eulerAngles;

        yRotation = angles.y;
        xRotation = angles.x;
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
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-movement, 0, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, movement, Space.Self);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -movement, Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, -movement, 0, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, movement, 0, Space.Self);
        }
        //Camera Rotation old
        /*if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);

            topDownCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.Self);

            topDownCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
            isometricCamera.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        }*/

        //Camera Rotation new
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeedx;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeedy;

            yRotation += mouseX;

            xRotation -= mouseY; ;
            xRotation = Mathf.Clamp(xRotation, -25F, 85F);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Zooming
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

        //stops cameras moving or zooming if the build panel is open
        if (panelOpen == true)
        {
            ScrollSpeed = 0f;
            lookSpeedx = 0;
            lookSpeedy = 0;

        }
        else
        {
            ScrollSpeed = 20f;
            lookSpeedx = 400F;
            lookSpeedy = 400F;
        }
    }
}
 