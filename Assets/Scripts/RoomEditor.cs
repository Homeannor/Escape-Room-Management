using UnityEngine;

public class RoomEditor : MonoBehaviour
{
    [SerializeField] private CanvasGroup Panel;

    // Commit comment

    public void OpenClosePanel()
    {
        Panel.alpha = Panel.alpha > 0 ? 0 : 1;
        Panel.interactable = Panel.interactable == true ? false : true; //this makes the panel only be interactable when it's visible so the buttons don't get clicked when the panel is closed
    }

    public void TestButtons()
    {
        Debug.Log("Button Pressed");
    }

    public void WoodenWallButton()
    {
        Debug.Log("Wooden Wall");
    }
    public void WoodenDoorButton()
    {
        Debug.Log("Wooden Door");
    }
    public void WoodenKeyButton()
    {
        Debug.Log("Wooden Key");
    }
    public void HorrorWallButton()
    {
        Debug.Log("Horror Wall");
    }
    public void HorrorDoorButton()
    {
        Debug.Log("Horror Door");
    }

    public void HorrorKeyButton()
    {
        Debug.Log("Horror Key");
    }
    public void TempleWallButton()
    {
        Debug.Log("Temple Wall");
    }
    public void TempleDoorButton()
    {
        Debug.Log("Temple Door");
    }
    public void TempleKeyButton()
    {
        Debug.Log("Temple Key");
    }

}
