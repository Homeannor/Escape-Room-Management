                         using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class placementsystem : MonoBehaviour
{
    [SerializeField]   
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private BuildingOverhaul BO;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private AssetDataBase database;
    private int selectedObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisual;

    [SerializeField]
  //private AudioSource source;

    private GridData floorData, PropData;

    private Renderer PreviewRenderer;

    private List <GameObject> PlacedGameObjects = new();

    [SerializeField]
    private UI RE;



    private void Start()
    {
        StopPlacement();
        floorData = new();
        PropData = new();
        PreviewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
        
    }

    public void StartPlacement(int ID)
    {
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
           Debug.Log("object with ID " + ID + " not found in database");
        }
        gridVisual.SetActive(true);
        cellIndicator.SetActive(true);
        BO.Onclicked += PlaceStructure;
        BO.OnExit += StopPlacement;
        RE.OpenClosePanel();
    }

    private void PlaceStructure()
    {
        if(BO.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePostion = BO.GetSelectedMapPosition();
        Vector3Int gridPostion = grid.WorldToCell(mousePostion);

        bool placementVaild = CheckPlacementVaild(gridPostion, selectedObjectIndex);
        if(placementVaild == false)
        {
            PreviewRenderer.material.color = placementVaild ? Color.white : Color.red;
            return;
        }
        //play auido here
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].PreFab);
        newObject.transform.position = grid.CellToWorld(gridPostion);
       
        PlacedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData : PropData;
        selectedData.AddObjectAt(gridPostion, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, PlacedGameObjects.Count - 1);

    }

    private bool CheckPlacementVaild(Vector3Int gridPosition, int objectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
            floorData :
            PropData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisual.SetActive(false);
        cellIndicator.SetActive(false);
        BO.Onclicked -= PlaceStructure;
        BO.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }

        Vector3 mousePostion = BO.GetSelectedMapPosition();
        Vector3Int gridPostion = grid.WorldToCell(mousePostion);

        bool placementVaild = CheckPlacementVaild(gridPostion, selectedObjectIndex);
        PreviewRenderer.material.color = placementVaild ? Color.white : Color.red;
       
        mouseIndicator.transform.position = mousePostion;
        cellIndicator.transform.position = grid.CellToWorld(gridPostion);
    }



}
