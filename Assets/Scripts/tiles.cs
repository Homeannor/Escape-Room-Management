using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class tiles : MonoBehaviour
{
    public static tiles instance;
    public Color hoverColour;
    private Color startColour;
    public Vector3 positionOffSet;

    public GameObject Build;
    public GameObject previewImage;
    public GameObject cancelButton;

    Buildmanager BM;

    private Renderer rend;

    private GameObject itemPrefabs;


    private void Start()
    {
        instance = this;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        BM = Buildmanager.instance;
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");

        previewImage = GameObject.FindWithTag("PreviewImage");
        cancelButton = GameObject.FindWithTag("CancelButton");
    }

    void Update()
    {
        if (BM.GetBuildingToBuild() != null)
        {
            //previewImage.SetActive(true);      
            cancelButton.SetActive(true);         
        }
        else
        {
            //previewImage.SetActive(false);      
            cancelButton.SetActive(false);
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




