using UnityEngine;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour
{

    public static EditorButton instance;
    private RoomEditor roomEditor;
    private GameObject itemPrefabs;
    private GameObject previewItemPrefabs;
    public GameObject previewImage;
    Buildmanager Bm;

    tiles T;
    [System.Obsolete]
    void Start()
    {
        instance = this;
        Bm = Buildmanager.instance;
        T = tiles.instance;
        roomEditor = FindObjectOfType<RoomEditor>();
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");
        previewItemPrefabs = GameObject.Find("== PREVIEW PREFABS ==");

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
