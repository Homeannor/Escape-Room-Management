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
    public Quaternion rotaion1;
    public Quaternion rotaion2;
    public Quaternion rotaion3;
    public Quaternion rotaion4;

    public bool Preview;



    public GameObject Build;

    public GameObject Previews;
    public GameObject previewImage;
    public GameObject cancelButton;
    public GameObject rotateButton;

    Buildmanager BM;

    private Renderer rend;
    public Material previewMaterial;

    private GameObject itemPrefabs;
    private GameObject previewItemPrefabs;


    private void Start()
    {
        instance = this;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        BM = Buildmanager.instance;
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");
        previewItemPrefabs = GameObject.Find("== PREVIEW PREFABS ==");

        previewImage = GameObject.FindWithTag("PreviewImage");
        cancelButton = GameObject.FindWithTag("CancelButton");
        rotateButton = GameObject.FindWithTag("RotateButton");
    }

    void Update()
    {
        if (BM.GetBuildingToBuild() != null)
        {
            //previewImage.SetActive(true);      
            cancelButton.SetActive(true);      
            rotateButton.SetActive(true);         
        }
        else
        {
            //previewImage.SetActive(false);      
            cancelButton.SetActive(false);
            rotateButton.SetActive(false);      
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

        placement(false);
    }

    private void OnMouseEnter()
    {
        if (BM.GetBuildingToBuild() == null)
        {
            return;
        }
        else
        {
            placement(true);
        }

        rend.material.color = hoverColour;
    }

    

    private void OnMouseExit()
    {
        rend.material.color = startColour;

        if (Previews != null)
        {
            GameObject.Destroy(Previews);
            Previews = null;
        }
    }
   
    private void placement(bool _preview)
    {
        if(!_preview)
        {
            if(BM.Angle == 0)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Build = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion1, itemPrefabs.transform);
                Build.SetActive(true);
            }
            else if (BM.Angle == 1)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Build = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion2, itemPrefabs.transform);
                Build.SetActive(true);
            }
            else if (BM.Angle == 2)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Build = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion3, itemPrefabs.transform);
                Build.SetActive(true);
            }
            else if (BM.Angle == 3)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Build = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion4, itemPrefabs.transform);
                Build.SetActive(true);
            }
        }
        else
        {
            if(BM.Angle == 0)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Previews = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion1, previewItemPrefabs.transform);
                Previews.SetActive(true);
            }
            else if (BM.Angle == 1)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Previews = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion2, previewItemPrefabs.transform);
                Previews.SetActive(true);
            }
            else if (BM.Angle == 2)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Previews = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion3, previewItemPrefabs.transform);
                Previews.SetActive(true);
            }
            else if (BM.Angle == 3)
            {
                GameObject BuildingToBuild = BM.GetBuildingToBuild();
                Previews = (GameObject)Instantiate(BuildingToBuild, transform.position + positionOffSet, transform.rotation * rotaion4, previewItemPrefabs.transform);
                Previews.SetActive(true);
            }
        }
    }


}




