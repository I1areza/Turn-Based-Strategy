using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int _width;
    private int _height;
    private int _cellSize;
    public GridSystem(int width, int height, int cellSize)
    {
        this._width = width;
        this._height = height;
        _cellSize = cellSize;
        
        for(int x = 0 ; x < width ; x++) 
        {
        for(int z = 0 ; z < height ; z++) 
            {
                Debug.DrawLine(GetWorldPosition(x,z)* _cellSize, GetWorldPosition(x,z)* _cellSize + Vector3.right*0.2f, Color.white, 10000);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int z) 
    {
        return new Vector3 (x, 0, z);
    }

    public GridCell GetGridPosition(int x, int z)
    {
        return new GridCell(x/_cellSize, z/_cellSize);
    }
}
