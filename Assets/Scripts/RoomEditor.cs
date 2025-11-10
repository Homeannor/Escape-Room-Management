using UnityEngine;

public class RoomEditor : MonoBehaviour
{
    [SerializeField] private CanvasGroup Panel;

    public void OpenClosePanel()
    {
        Panel.alpha = Panel.alpha > 0 ? 0 : 1;
        Panel.interactable = Panel.interactable == true ? false : true; //this makes the panel only be interactable when it's visible so the buttons don't get clicked when the panel is closed
    }

    public void TestButtons()
    {
        Debug.Log("Button Pressed");
    }

}
 
