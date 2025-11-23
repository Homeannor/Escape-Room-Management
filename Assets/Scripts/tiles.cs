using UnityEngine;

public class tiles : MonoBehaviour
{
    public Color hoverColour;
    private Color startColour;
    public Vector3 positionOffSet;

    public GameObject Build;

    Buildmanager BM;

    private Renderer rend;

    private GameObject itemPrefabs;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        BM = Buildmanager.instance;
        itemPrefabs = GameObject.Find("== ITEM PREFABS ==");
    }

    private void OnMouseDown()
    {
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
        BM.SetBuildingTobuild(null);


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




