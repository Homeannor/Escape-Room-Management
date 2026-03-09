using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;


public class BuildingOverhaul : MonoBehaviour
{

    [SerializeField]
    public Camera sceneCamera;

    private Vector3 lastPostion;

    [SerializeField]
    private LayerMask placementLayermask;

    public event Action Onclicked, OnExit, Rotation;

    [SerializeField]
    private placementsystem PS;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Onclicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rotation?.Invoke();
            PS.HasMoved = true;
        }
    }

    public void cancelButton()
    {
        OnExit?.Invoke();
    }

    public void RotateButton()
    {
        Rotation?.Invoke();
    }


    public bool IsPointerOverUI()
     =>  EventSystem.current.IsPointerOverGameObject();
   


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
