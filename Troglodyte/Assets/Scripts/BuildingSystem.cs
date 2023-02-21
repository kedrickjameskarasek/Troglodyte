using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Xml.Serialization;
using UnityEditor;
using Unity.VisualScripting;

public class BuildingSystem : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject pendingObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    public InputAction place;

    void Update()
    {
       if(pendingObject != null)
        {
            pendingObject.transform.position = pos;
        }
    }

    private void OnEnable()
    {
        place.Enable();
        place.performed += placed;
    }

    private void OnDisable()
    {
        place.Disable(); 
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }

    }

    public void SelectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);
    }

    private void placed(InputAction.CallbackContext contex)
    {
        pendingObject = null;
    }
}
