using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tiles : MonoBehaviour
{
    public Color hoverColour;
    private Color startColour;
    public Vector3 positionOffSet;

    public GameObject Build;
    public GameObject previewImage;

    Buildmanager BM;

    private Renderer rend;

    private GameObject itemPrefabs;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        BM = Buildmanager.instance;
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");
        previewImage = GameObject.FindWithTag("PreviewImage");
    }

    void Update()
    {
        if (Build != null && Input.GetKeyDown(KeyCode.P))
        {
            previewImage.SetActive(false);
            BM.SetBuildingTobuild(null);            
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (BM.GetBuildingToBuild() == null)
        {
            return;
        }

        if (Build != null)
        {
            Debug.Log("cant build here");
            return;
        }

        GameObject BuildingToBuild = BM.GetBuildingToBuild();
        Build = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation, itemPrefabs.transform);
        Build.SetActive(true);
    }

    private void OnMouseEnter()
    {
        if (BM.GetBuildingToBuild() == null)
        {
            return;
        }

        rend.material.color = hoverColour;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColour;
    }




}




