using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GridObject
{
    private GridSystem _gridSystem;
    private GridPosition _gridCell;
    private List<Unit> _unitList;

    

    public GridObject(GridSystem gridSystem, GridPosition gridCell)
    {
        _gridSystem = gridSystem;
        _gridCell = gridCell;
        _unitList = new List<Unit>();
    }

    public void AddUnit(Unit unit) 
    {
        _unitList.Add(unit);
    }

    public List<Unit> GetUnitList() 
    {
        return _unitList;
    }

    public void RemoveUnit(Unit unit) 
    {
        _unitList.Remove(unit);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Unit unit in GetUnitList()) 
        {
            stringBuilder.Append(unit.ToString()+" ");
        }
        
        return $"{_gridCell.x},{_gridCell.z}\n{stringBuilder.ToString()}";
    }
    public bool HasUnits()
    {
        return _unitList.Count > 0;
    }

}
