using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomEditor : MonoBehaviour
{
    [SerializeField] private CanvasGroup Panel;
    public GameObject directionalLight;

    private string timeOfDay = "Day";
    public GameObject timeButton;
    public TextMeshProUGUI timeButtonText;

    public void OpenClosePanel()
    {
        Panel.alpha = Panel.alpha > 0 ? 0 : 1;
        Panel.interactable = Panel.interactable == true ? false : true; //this makes the panel only be interactable when it's visible so the buttons don't get clicked when the panel is closed
    }

    void Start()
    {
        directionalLight.transform.rotation = Quaternion.Euler(130f, -10f, 0f);
        timeButton.GetComponent<Image>().color = new Color32(255, 255, 175, 255);
        timeButtonText.text = "DAYTIME";
    }

    public void timeToggle()
    {
        if (timeOfDay == "Day")
        {
            timeOfDay = "Evening";
            directionalLight.transform.rotation = Quaternion.Euler(-3, -10f, 0f);
            timeButton.GetComponent<Image>().color = new Color32(255, 200, 100, 255);
            timeButtonText.text = "EVENING";
        }
        else if (timeOfDay == "Evening")
        {
            timeOfDay = "Night";
            directionalLight.transform.rotation = Quaternion.Euler(-50, -10f, 0f);
            timeButton.GetComponent<Image>().color = new Color32(50, 50, 50, 255);
            timeButtonText.text = "NIGHT";
        }
        else if (timeOfDay == "Night")
        {
            timeOfDay = "Day";
            directionalLight.transform.rotation = Quaternion.Euler(130f, -10f, 0f);
            timeButton.GetComponent<Image>().color = new Color32(255, 255, 175, 255);
            timeButtonText.text = "DAYTIME";
        }
    }

    /*public void RoomEditorButton()
    {
        Debug.Log("Item Selected: " + itemPrefab.name);
        Bm.SetBuildingTobuild(itemPrefab);
        OpenClosePanel();
    }*/

}

