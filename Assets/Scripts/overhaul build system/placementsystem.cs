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

    private GridData floorData, FloorPropData, WallData, WallPropData;

    private List <GameObject> PlacedGameObjects = new();
    [SerializeField] private Image previewImage;

    [SerializeField]
    private UI RE;

    public GameObject buildOptions;

    public int Angle = 0;

    [SerializeField]
    
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;
    [SerializeField]
    public Quaternion offset1;
    public Quaternion offset2;
    public Quaternion offset3;
    public Quaternion offset4;
    private void Start()
    {
        StopPlacement();
        floorData = new();
        FloorPropData = new();
        WallData = new();
        WallPropData = new();
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
        BO.Rotation += Rotate;
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
        GameObject rotatePoint = newObject.transform.GetChild(0).gameObject;
        if (Angle == 0 )
        {
            rotatePoint.transform.rotation *= offset1;
        }
        else if (Angle == 1)
        {
            rotatePoint.transform.rotation *= offset2;
        }
        else if (Angle == 2)
        {
            rotatePoint.transform.rotation *= offset3;
        }
        else if (Angle == 3)
        {
            rotatePoint.transform.rotation *= offset4;
        }


        PlacedGameObjects.Add(newObject);
        GridData selectedData = database.objectsData[selectedObjectIndex].IsFloor == true ?
           floorData : FloorPropData;
        selectedData.AddObjectAt(gridPostion, database.objectsData[selectedObjectIndex].Size, database.objectsData[selectedObjectIndex].ID, PlacedGameObjects.Count - 1);
        preview.UpdatePosition(grid.CellToWorld(gridPostion), false);
    }

    private bool CheckPlacementVaild(Vector3Int gridPosition, int objectIndex)
    {
        GridData selectedData = database.objectsData[selectedObjectIndex].IsFloor == true ?
            floorData : FloorPropData;

        return selectedData.CanPlaceObjectAt(gridPosition, database.objectsData[selectedObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisual.SetActive(false);
        preview.StopShowingPreview();
        BO.Onclicked -= PlaceStructure;
        BO.OnExit -= StopPlacement;
        BO.Rotation -= Rotate;
        lastDetectedPosition = Vector3Int.zero;



        previewImage.color = new Color(1f, 1f, 1f, 0f);

        buildOptions.SetActive(false);
    }

    private void Rotate()
    {
        Angle += 1;

        if (Angle == 4)
        {
            Angle = 0;
        }
       
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
