using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject isometricCamera;
    public GameObject topDownCamera;
    public int Manager;

    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }

    
    public void ManageCamera()
    {
        if(Manager == 0)
        {
            Cam_TopDown();
            Manager = 1;
        }
        else
        {
            Cam_Main();
            Manager = 0;
        }
    } 

    void Cam_Main()
    {
        isometricCamera.SetActive(true);
        topDownCamera.SetActive(false);
        isometricCamera.GetComponent<AudioListener>().enabled = true;
        topDownCamera.GetComponent<AudioListener>().enabled = false;
    }
    void Cam_TopDown()
    {
        isometricCamera.SetActive(false);
        topDownCamera.SetActive(true);
        isometricCamera.GetComponent<AudioListener>().enabled = false;
        topDownCamera.GetComponent<AudioListener>().enabled = true;
    }
}
 