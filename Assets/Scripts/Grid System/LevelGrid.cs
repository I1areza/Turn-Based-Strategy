using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{


    [SerializeField] private GameObject _debugGridObject;
    private GridSystem _gridSystem;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance of LevelGrid: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _gridSystem = new GridSystem(10, 10, 2, Vector3.zero);
        _gridSystem.CreateDebugObjects(_debugGridObject);
    }

    public static LevelGrid Instance { get; private set; }
    
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        _gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        _gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) 
    {
        return _gridSystem.GetGridObject(gridPosition).GetUnitList();
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition from, GridPosition to) 
    {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);
    public int GetWidth() => _gridSystem.Width;
    public int GetHeight() => _gridSystem.Height;
    public bool IsValidGridPosition(GridPosition position) => _gridSystem.IsValidGridPosition(position);
    


    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) => _gridSystem.GetGridObject(gridPosition).HasUnits();

}
