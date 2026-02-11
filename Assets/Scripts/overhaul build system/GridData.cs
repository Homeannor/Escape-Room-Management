using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObject = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectsize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectsize);
        PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex);
        foreach (var pos in positionToOccupy)
        {
            if (placedObject.ContainsKey(pos))
            {

            }
            placedObject[pos] = data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectsize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectsize.x; x++)
        {
            for (int y = 0; y < objectsize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }

        }
        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectsize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectsize);
        foreach (var pos in positionToOccupy)
        {
            if (placedObject.ContainsKey(pos))
                return false;
        }
        return true;
    }
}
public class PlacementData
{
    public List<Vector3Int> occupiedPosition;

    public int ID { get; private set; }

    public int PlacementObjectIndex { get; private set; }


    public PlacementData(List<Vector3Int> occupiedPosition, int ID, int placementObjectIndex)
    {
        this.occupiedPosition = occupiedPosition;
        ID = ID;
        PlacementObjectIndex = placementObjectIndex;
    }


}
