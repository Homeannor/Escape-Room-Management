using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "AssetDataBase", menuName = "Scriptable Objects/AssetDataBase")]
public class AssetDataBase : ScriptableObject
{
    public List<ObjectData> objectsData;

}

[Serializable]

public record ObjectData(string Name, int ID, GameObject PreFab)
{
    [field: SerializeField]

    public string Name { get; private set; } 
    [field: SerializeField]

    public int ID { get; private set; }
    [field: SerializeField]

    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]

    public GameObject PreFab { get; private set; }

}
