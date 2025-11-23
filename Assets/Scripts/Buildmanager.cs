using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
public class Buildmanager : MonoBehaviour
{
    public GameObject BuildingToBuild;
    public static Buildmanager instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one build maganer in scene");
            return;
        }

        instance = this;
    }

    public void SetBuildingTobuild(GameObject Building)
    {
        BuildingToBuild = Building;
    }

    public GameObject GetBuildingToBuild()
    {
        return BuildingToBuild;
    }
}


