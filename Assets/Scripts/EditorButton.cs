using UnityEngine;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour
{
    private RoomEditor roomEditor;
    private GameObject itemPrefabs;
    public GameObject previewImage;
    Buildmanager Bm;

    [System.Obsolete]
    void Start()
    {
        Bm = Buildmanager.instance;
        roomEditor = FindObjectOfType<RoomEditor>();
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");

        previewImage = GameObject.FindWithTag("PreviewImage");
        //previewImage.SetActive(false);
        //previewImage.gameObject.SetActive(false);

        Debug.Log("Current Item: " + itemPrefabs.transform.Find(gameObject.name));
    }

    public void RoomEditorButton()
    {
        previewImage.SetActive(true);
        previewImage.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite;
        Debug.Log("Item Selected: " + itemPrefabs.transform.Find(gameObject.name));
        Bm.SetBuildingTobuild(itemPrefabs.transform.Find(gameObject.name).gameObject);
        roomEditor.OpenClosePanel();
    }
}
