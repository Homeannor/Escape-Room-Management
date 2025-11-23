using UnityEngine;

public class EditorButton : MonoBehaviour
{
    private RoomEditor roomEditor;
    private GameObject itemPrefabs;
    Buildmanager Bm;

    void Start()
    {
        Bm = Buildmanager.instance;
        roomEditor = FindObjectOfType<RoomEditor>();
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");

        Debug.Log("Current Item: " + itemPrefabs.transform.Find(gameObject.name));
    }

    public void RoomEditorButton()
    {
        Debug.Log("Item Selected: " + itemPrefabs.transform.Find(gameObject.name));
        Bm.SetBuildingTobuild(itemPrefabs.transform.Find(gameObject.name).gameObject);
        roomEditor.OpenClosePanel();
    }
}
