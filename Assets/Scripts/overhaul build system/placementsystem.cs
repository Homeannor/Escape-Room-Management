using UnityEngine;

public class placementsystem : MonoBehaviour
{
    [SerializeField]
    private GameObject mouseIndicator, cellIndicator;
    [SerializeField]
    private BuildingOverhaul BO;
    [SerializeField]
    private Grid grid;



    private void Update()
    {
        Vector3 mousePostion = BO.GetSelectedMapPosition();
        Vector3Int gridPostion = grid.WorldToCell(mousePostion);
        mouseIndicator.transform.position = mousePostion;
        cellIndicator.transform.position = grid.CellToWorld(gridPostion);
    }



}
