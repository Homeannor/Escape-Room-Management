using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class RoomEditor : MonoBehaviour
{
    [SerializeField] private GameObject PanelCanvas;
    public GameObject directionalLight;

    private string timeOfDay = "Day";
    public GameObject timeButton;
    public TextMeshProUGUI timeButtonText;
    Buildmanager BM;



    //tiles tileInstance;

    public void OpenClosePanel()
    {
        if (PanelCanvas.activeSelf)
        {
            //Debug.Log($"PanelCanvas Active: {PanelCanvas.activeSelf}");
            PanelCanvas.SetActive(false);
            //Debug.Log("PanelCanvas set to false");
        }
        else
        {
            //Debug.Log($"PanelCanvas Active: {PanelCanvas.activeSelf}");
            PanelCanvas.SetActive(true);
            //Debug.Log("PanelCanvas set to true");
        }

        //PanelCanvas.SetActive(true ? false : true);
        //Debug.Log("Changing the PanelCanvas Visibility");
        //Panel.alpha = Panel.alpha > 0 ? 0 : 1;
        //PanelCanvas.GetComponent<CanvasGroup>().interactable = PanelCanvas.GetComponent<CanvasGroup>().interactable == true ? false : true; //this makes the panel only be interactable when it's visible so the buttons don't get clicked when the panel is closed
    }

    void Start()
    {
        directionalLight.transform.rotation = Quaternion.Euler(130f, -10f, 0f);
        timeButton.GetComponent<Image>().color = new Color32(255, 255, 175, 255);
        timeButtonText.text = "DAYTIME";
        BM = Buildmanager.instance;// link to build mager
        //tileInstance = tiles.instance;
    }

    void Update()
    {
        if (BM.GetBuildingToBuild() != null && Input.GetKeyDown(KeyCode.Escape))
        {
            cancelButton();
            
            return;
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (BM.GetBuildingToBuild() != null && Input.GetKeyDown(KeyCode.R))
        {
            rotateButton();
        }
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

    public void cancelButton()
    {
        tiles.instance.previewImage.SetActive(false);
        tiles.instance.Previews = null;
        Buildmanager.instance.SetBuildingTobuild(null);   
    }

    public void rotateButton()
    {
        BM.Anglechanges();
    }

  

    /*public void RoomEditorButton()
    {
        Debug.Log("Item Selected: " + itemPrefab.name);
        Bm.SetBuildingTobuild(itemPrefab);
        OpenClosePanel();
    }*/

}

