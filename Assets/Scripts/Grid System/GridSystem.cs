using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int _width;
    private int _height;
    private int _cellSize;
    private Vector3 _gridOffset;
    private GridObject[,] _gridObjectsArray;


    public int Width { get { return _width; } }
    public int Height { get { return _height; } }
    public GridSystem(int width, int height, int cellSize, Vector3 gridOffset)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _gridOffset = gridOffset;
        _gridObjectsArray = new GridObject[width, height];

        for (int x = 0 ; x < width ; x++) 
        {
            for(int z = 0 ; z < height ; z++) 
            {
                _gridObjectsArray[x, z] = new GridObject(this, new GridPosition(x, z));
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition) 
    {
        return new Vector3 (gridPosition.x, 0, gridPosition.z) *_cellSize + _gridOffset;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        worldPosition -= _gridOffset;
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / _cellSize), Mathf.RoundToInt(worldPosition.z / _cellSize));
    }


    public void CreateDebugObjects(GameObject prefab) 
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                GridPosition gridCell = new GridPosition(x, z);
                GameObject debugObject = GameObject.Instantiate(prefab, GetWorldPosition(gridCell), Quaternion.identity);
                var _gridDebugObject = debugObject.GetComponent<GridDebugObject>();
                _gridDebugObject.SetGridObject(GetGridObject(gridCell));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) 
    {
        return _gridObjectsArray[gridPosition.x, gridPosition.z];
    }

    public bool IsValidGridPosition(GridPosition position) 
    {
         return position.x >= 0 &&
                position.x < _width &&
                position.z >= 0 &&
                position.z < _height;
    }
}
