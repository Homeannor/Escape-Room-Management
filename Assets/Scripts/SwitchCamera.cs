using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject Camera_Main;
    public GameObject Camera_TopDown;
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
        Camera_Main.SetActive(true);
        Camera_TopDown.SetActive(false);
    }
    void Cam_TopDown()
    {
        Camera_Main.SetActive(false);
        Camera_TopDown.SetActive(true);
    }
}
