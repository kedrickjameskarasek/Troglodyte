using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    bool toggleMenu = false;
    public GameObject[] objects;
    private GameObject pendingObject;
    public GameObject menu;
    private Vector3 pos;
    private RaycastHit hit;
    public MenuAction menuControls;
    private InputAction open;
    [SerializeField] private LayerMask layerMask;


    // Start is called before the first frame update
    private void Awake()
    {
        menuControls = new MenuAction();
    }
    void Start()
    {
       
    }

    private void OnEnable()
    {
        open = menuControls.Menu.openMenu;
        open.Enable();
        open.performed += opened;


    }


    private void OnDisable()
    {
        open.Disable();

    }

    // Update is called once per frame
    void Update()
    {

        
        /*if (pendingObject != null)
        {
            pendingObject.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                placeObject();
            }

        }*/



    }

    public void placeObject()
    {
        pendingObject = null;

    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit, 1000, layerMask))
        {
            pos = hit.point;
        }

    }

    private void selectObject(int index)
    {
        pendingObject = Instantiate(objects[index], pos, transform.rotation);

    }

    private void opened(InputAction.CallbackContext contex)
    {
        toggleMenu = !toggleMenu;
        menu.SetActive(toggleMenu);
    }
}
