using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    [SerializeField]
    private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;
    [SerializeField]
    private placementsystem PS;


    
    public float startx;
    public float starty;
    public float startz;

    private bool OnlyOnce = true;




    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartingShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
       

    }
    
    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1f, size.y);
            cellIndicatorRenderer.material.mainTextureScale = size;
        }
    }

    private void PreparePreview(GameObject PreviewObject)
    {
        Renderer[] renderers = PreviewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }
    }


    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        Destroy(previewObject);


    }


    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
        RotatePreview(position);
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        cellIndicatorRenderer.material.color = c;
        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y + previewYOffset, position.z);
        

    }

    public void RotatePreview(Vector3 position)
    {




        GameObject rotatePoint = previewObject.transform.GetChild(0).gameObject;


        if (PS.OffTile == false)
        {
            if (PS.Angle == 0)
            {
                rotatePoint.transform.rotation = PS.offset1;
                if (PS.HasMoved == true)
                {
                    rotatePoint.transform.position = new Vector3(startx, starty, startz );
                    PS.HasMoved = false;

                }
            }
            else if (PS.Angle == 1)
            {
                rotatePoint.transform.rotation = PS.offset2;
                if (PS.HasMoved == true)
                {
                    rotatePoint.transform.position = new Vector3(startx, starty, startz + 1f);
                    PS.HasMoved = false;

                }
            }
            else if (PS.Angle == 2)
            {
                rotatePoint.transform.rotation = PS.offset3;
                if (PS.HasMoved == true)
                {
                    rotatePoint.transform.position = new Vector3(startx + 1f, starty, startz);
                    PS.HasMoved = false;

                }
            }
            else if (PS.Angle == 3)
            {
                rotatePoint.transform.rotation = PS.offset4;
                if (PS.HasMoved == true)
                {
                    rotatePoint.transform.position = new Vector3(startx, starty, startz );
                    PS.HasMoved = false;

                }
            }
        }
        if (PS.OffTile == true)
        {
            startx = rotatePoint.transform.position.x;
            starty = rotatePoint.transform.position.y;
            startz = rotatePoint.transform.position.z;
            PS.OffTile = false;
            
        }
    }

}
