using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public int objectIndex;

    public float posX, posY, posZ;
    public float rotX, rotY, rotZ;
}

[System.Serializable]
public class RoomData
{
    public List<ItemData> items = new List<ItemData>();
}

public class SavingManager : MonoBehaviour
{
    [Header("References")]
    public Transform roomContainer;

    public AssetDataBase database;

    // =========================
    // SAVE
    // =========================
    public void SaveRoom(int roomIndex)
    {
        Debug.Log($"--- START SAVING ROOM {roomIndex} ---");

        RoomData roomData = new RoomData();

        int count = 0;

        foreach (Transform item in roomContainer)
        {
            PlaceableItem placeable = item.GetComponent<PlaceableItem>();

            if (placeable == null)
            {
                Debug.LogWarning($"Skipping object (no PlaceableItem): {item.name}");
                continue;
            }

            ItemData data = new ItemData();

            data.objectIndex = placeable.objectIndex;

            data.posX = item.position.x;
            data.posY = item.position.y;
            data.posZ = item.position.z;

            GameObject rotatePoint = item.transform.GetChild(0).gameObject;

            data.rotX = rotatePoint.transform.eulerAngles.x;
            data.rotY = rotatePoint.transform.eulerAngles.y;
            data.rotZ = rotatePoint.transform.eulerAngles.z;

            roomData.items.Add(data);

            Debug.Log($"Saved Item #{count}: {item.name} | Index: {data.objectIndex} | Pos: {item.position}");

            count++;
        }

        string json = JsonUtility.ToJson(roomData, true); // pretty print

        Debug.Log($"FINAL JSON:\n{json}");

        PlayerPrefs.SetString("ROOM_" + roomIndex, json);
        PlayerPrefs.Save();

        Debug.Log($"--- FINISHED SAVING ROOM {roomIndex} | Total Items: {count} ---");
    }

    // =========================
    // LOAD
    // =========================
    public void LoadRoom(int roomIndex)
    {
        Debug.Log($"--- START LOADING ROOM {roomIndex} ---");
        // Debug.Log("objectsData count: " + database.objectsData.Count);

        string key = "ROOM_" + roomIndex;

        if (!PlayerPrefs.HasKey(key))
        {
            Debug.LogWarning($"No save found for Room {roomIndex}");
            return;
        }

        string json = PlayerPrefs.GetString(key);

        Debug.Log($"LOADED JSON:\n{json}");

        RoomData roomData = JsonUtility.FromJson<RoomData>(json);

        // Clear existing objects
        int deleted = 0;

        for (int i = roomContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(roomContainer.GetChild(i).gameObject);
            deleted++;
        }

        Debug.Log($"Cleared {deleted} existing objects");

        // Recreate objects
        int count = 0;
        
        foreach (ItemData data in roomData.items)
        {

            if (data.objectIndex < 0 || data.objectIndex >= database.objectsData.Count)
            {
                Debug.LogWarning($"Invalid object index: {data.objectIndex}");
                continue;
            }

            GameObject prefab = database.objectsData[data.objectIndex].PreFab;

            Vector3 pos = new Vector3(data.posX, data.posY, data.posZ);

            GameObject rotatePoint = prefab.transform.GetChild(0).gameObject;

            Quaternion rot = Quaternion.Euler(data.rotX, data.rotY, data.rotZ);

            GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
            obj.transform.GetChild(0).gameObject.transform.rotation = rot;
            obj.transform.SetParent(roomContainer);

            PlaceableItem placeable = obj.GetComponent<PlaceableItem>();
            if (placeable != null)
            {
                placeable.objectIndex = data.objectIndex;
            }

            Debug.Log($"Loaded Item #{count}: {prefab.name} | Index: {data.objectIndex} | Pos: {pos}");

            count++;
        }

        Debug.Log($"--- FINISHED LOADING ROOM {roomIndex} | Total Items: {count} ---");
    }

    public void DeleteRoom(int roomIndex)
    {
        PlayerPrefs.DeleteKey("ROOM_" + roomIndex);
        Debug.Log($"Deleted Room {roomIndex}");
    }
}