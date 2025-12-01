using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchCamera : MonoBehaviour
{
    public GameObject isometricCamera;
    public GameObject topDownCamera;
    public GameObject cameraButton;
    public TextMeshProUGUI cameraButtonText;
    public int Manager;

    void Start()
    {
        cameraButton.GetComponent<Image>().color = new Color32(255, 204, 211, 255);
        cameraButtonText.text = "TOP DOWN CAMERA";
    }
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
            cameraButton.GetComponent<Image>().color = new Color32(255, 204, 211, 255);
            cameraButtonText.text = "ISOMETRIC CAMERA";
        }
        else
        {
            Cam_Isometeric();
            Manager = 0;
            cameraButton.GetComponent<Image>().color = new Color32(255, 204, 211, 255);
            cameraButtonText.text = "TOP DOWN CAMERA";
        }
    } 

    void Cam_Isometeric()
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
 