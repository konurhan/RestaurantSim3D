using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;


    public Tilemap dragDropLayer;
    public Grid grid;

    private bool[,] availability;

    private void Awake()
    {
        Instance = this;
        grid = GetComponent<Grid>();
    }

    public void SaveGridState()
    {

    }
}
