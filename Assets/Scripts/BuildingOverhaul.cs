using UnityEngine;

public class BuildingOverhaul : MonoBehaviour
{

    [SerializeField]
    private Camera sceneCamera;

    private Vector3 lastPostion;

    [SerializeField]
    private LayerMask placementLayermask;

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayermask))
        {
            lastPostion = hit.point;
        }
        return lastPostion;
    }




   
}
