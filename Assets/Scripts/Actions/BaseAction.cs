using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit _unit;
    protected bool _isActive;
    protected Action _onActionComplete;
    protected int _actionCost;
    public int ActionPointsCost { get { return _actionCost; } }

    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition position, Action OnActionComplete);

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) 
    {
        var validGridPosition = GetValidActionGridPositionList();
        return validGridPosition.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();
}
