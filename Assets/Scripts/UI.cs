using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class UI : MonoBehaviour
{
    [SerializeField] private GameObject PanelCanvas;
    public GameObject directionalLight;

    private string timeOfDay = "Day";
    public GameObject timeButton;
    public TextMeshProUGUI timeButtonText;
    [SerializeField]

    public Transform placedItemFolder;

    // Undo and Redo
    public Stack<GameObject> undoStack = new Stack<GameObject>();
    public Stack<GameObject> redoStack = new Stack<GameObject>();
    public Button undoButton;
    public Button redoButton;

    public void OpenClosePanel()
    {
        if (PanelCanvas.activeSelf)
        {
            //Debug.Log($"PanelCanvas Active: {PanelCanvas.activeSelf}");
            PanelCanvas.SetActive(false);
            CameraMovement.instance.panelOpen = false;
            //Debug.Log("PanelCanvas set to false");
        }
        else
        {
            //Debug.Log($"PanelCanvas Active: {PanelCanvas.activeSelf}");
            PanelCanvas.SetActive(true);
            CameraMovement.instance.panelOpen = true;
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

        undoStack.Clear();
        redoStack.Clear();
    }

    void Update()
    {
        UpdateUndoRedoUI();
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

    public void ClearRoom()
    {
        foreach (Transform item in placedItemFolder)
        {
            Destroy(item.gameObject);
        }

        undoStack.Clear();
        redoStack.Clear();
    }

    public void Undo()
    {
        if (undoStack.Count == 0) return;

        GameObject lastItem = undoStack.Pop();

        lastItem.SetActive(false);

        redoStack.Push(lastItem);
    }

    public void Redo()
    {
        if (redoStack.Count == 0) return;

        GameObject item = redoStack.Pop();

        item.SetActive(true);

        undoStack.Push(item);
    }

    void UpdateUndoRedoUI()
    {
        // Undo button
        if (undoStack.Count == 0)
        {
            undoButton.interactable = false;
        }
        else
        {
            undoButton.interactable = true;
        }

        // Redo button
        if (redoStack.Count == 0)
        {
            redoButton.interactable = false;
        }
        else
        {
            redoButton.interactable = true;
        }
    }
}

