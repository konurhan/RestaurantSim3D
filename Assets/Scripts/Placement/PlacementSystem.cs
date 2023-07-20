using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour
{
    public static PlacementSystem Instance;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] Tilemap mainTileMap;
    [SerializeField] TileBase whiteTile;

    public GameObject tableWithChairs;

    public PlacableObject objectToPlace;

    public GameObject navmesh;
    public GameObject navmesh2;

    public bool isPlacementActive;

    private void Awake()
    {
        Instance = this;
        grid = gridLayout.GetComponent<Grid>();
        
    }

    private void Start()
    {
        //BakeNavMeshSurface();
        isPlacementActive = false;
    }

    private void OnEnable()
    {
        //navmesh = RestaurantManager.Instance.navmesh;
        //navmesh2 = RestaurantManager.Instance.navmesh2;
        
    }

    private void Update()
    {
        if (!isPlacementActive) return;
        RotateObjectToPlace();
    }

    #region utilities
    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Debug.Log("mouse world position is: " + raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapPositionToGrid(Vector3 position)//get position of an object and return the position as if it is snapped to the grid 
    {
        Vector3Int cellPos = grid.WorldToCell(position);
        Debug.Log("cellPos: " + cellPos);
        position = grid.GetCellCenterWorld(cellPos);
        Debug.Log("cell to world pos: " + position);
        position += Vector3.up * 0.09f;
        return position;
    }
    #endregion

    public void InitializeTable()//bind to a button click: + button to add new table buradan devAM
    {
        RestaurantManager.Instance.money -= 200;
        Vector3 position = SnapPositionToGrid(new Vector3(15.25f, 0, 18f));
        //position += Vector3.up * 1.1f;

        GameObject obj = Instantiate(tableWithChairs, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlacableObject>();
        objectToPlace.isPlaced = false;//start new tables as not placed
        obj.AddComponent<ObjectDrag>();
        obj.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(8));//set parent to unsaved tables
    }

    public void PlaceObject()//should be still called by space key --> or automatically called every time mouse is released
    {
        objectToPlace.Place();
        objectToPlace = null;
    }

    public void StartPlacemnet()//make all existing tables dragable
    {
        Transform tables = RestaurantManager.Instance.RestaurantComponents.GetChild(1);
        for (int i = 0; i < tables.childCount; i++)
        {
            tables.GetChild(i).gameObject.AddComponent<ObjectDrag>();
            tables.GetChild(i).gameObject.GetComponent<PlacableObject>().isPlaced = true;
        }
    }

    public void BakeNavMeshSurface()
    {
        NavMeshSurface surface = navmesh.GetComponent<NavMeshSurface>();
        NavMeshSurface surface2 = navmesh2.GetComponent<NavMeshSurface>();

        surface.UpdateNavMesh(surface.navMeshData);
        surface2.UpdateNavMesh(surface2.navMeshData);
    }

    public void FinalizePlacement()//bind to a button click: tick button to verify changes
    {
        Transform tables = RestaurantManager.Instance.RestaurantComponents.GetChild(1);
        Transform unsavedTables = RestaurantManager.Instance.RestaurantComponents.GetChild(8);
        
        for (int i = unsavedTables.childCount-1; i >= 0; i--)
        {
            Transform t = unsavedTables.GetChild(i);
            if (t.gameObject.GetComponent<PlacableObject>().isPlaced == false) 
            {
                RestaurantManager.Instance.money += 200;//return back the money 
                Destroy(t.gameObject);
                return;
            } 
            t.gameObject.transform.SetParent(tables);//adding table to tables in hierarchy
        }
        
        for (int i = 0; i < tables.childCount; i++)
        {
            Transform t = tables.GetChild(i);
            Destroy(t.gameObject.GetComponent<ObjectDrag>());
        }

        RestaurantManager.Instance.CacheSeats();
        RestaurantManager.Instance.CacheTables();
        BakeNavMeshSurface();
    }

    public void RotateObjectToPlace()
    {
        if (!objectToPlace) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 eulerAngles = new Vector3(0, 45, 0);
            objectToPlace.transform.Rotate(eulerAngles,Space.World);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 eulerAngles = new Vector3(0, -45, 0);
            objectToPlace.transform.Rotate(eulerAngles, Space.World);
        }
    }
}
