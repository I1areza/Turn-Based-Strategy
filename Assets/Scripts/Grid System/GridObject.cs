using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSysteml;
    private GridPosition _gridCell;

    public GridObject(GridSystem gridSystem, GridPosition gridCell)
    {
        _gridSysteml = gridSystem;
        _gridCell = gridCell;
    }

    public override string ToString()
    {
        return $"{_gridCell.x},{_gridCell.z}";
    }
}
