using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class placementsystem : MonoBehaviour
{
    [SerializeField]   
    private GameObject mouseIndicator;
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

    private List <GameObject> PlacedGameObjects = new();
    [SerializeField] private Image previewImage;

    [SerializeField]
    private UI RE;

    public GameObject buildOptions;

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    private void Start()
    {
        StopPlacement();
        floorData = new();
        PropData = new();
        buildOptions.SetActive(false);
        
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
           Debug.Log("object with ID " + ID + " not found in database");
        }
        gridVisual.SetActive(true);
        preview.StartingShowingPlacementPreview(database.objectsData[selectedObjectIndex].PreFab, database.objectsData[selectedObjectIndex].Size);
        BO.Onclicked += PlaceStructure;
        BO.OnExit += StopPlacement;
        RE.OpenClosePanel();

        previewImage.color = new Color(1f, 1f, 1f, 1f);
        previewImage.sprite = database.objectsData[selectedObjectIndex].itemImage;

        buildOptions.SetActive(true);
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
            //play auido here for not vaild
            return;
        }
        //play auido here for vaild
        GameObject newObject = Instantiate(database.objectsData[selectedObjectIndex].PreFab);
        newObject.transform.position = grid.CellToWorld(gridPostion);
       
        PlacedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].ID == 0 ?
           floorData : PropData;
        selectedData.AddObjectAt(gridPostion, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, PlacedGameObjects.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPostion), false);
;    }

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
        preview.StopShowingPreview();
        BO.Onclicked -= PlaceStructure;
        BO.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;



        previewImage.color = new Color(1f, 1f, 1f, 0f);

        buildOptions.SetActive(false);
    }

    private void Update()
    {
        if (selectedObjectIndex < 0)
        {
            return;
        }

        Vector3 mousePostion = BO.GetSelectedMapPosition();
        Vector3Int gridPostion = grid.WorldToCell(mousePostion);
        if (lastDetectedPosition != gridPostion)
        {
            bool placementVaild = CheckPlacementVaild(gridPostion, selectedObjectIndex);

            mouseIndicator.transform.position = mousePostion;
            preview.UpdatePosition(grid.CellToWorld(gridPostion), placementVaild);
            lastDetectedPosition = gridPostion;
        }
       
    }

    

}
