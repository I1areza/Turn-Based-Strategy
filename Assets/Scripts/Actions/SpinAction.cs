using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float _totalSpinAmount;
    public delegate void SpinCompleteDelegate();
    private SpinCompleteDelegate _onSpinComplete;


    protected override void Awake()
    {
        base.Awake();
        _actionCost = 2;
    }
    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete) 
    {
        _onActionComplete += OnActionComplete;
        _isActive = true;
        _totalSpinAmount = 0f;
    }
    
    void Update()
    {
        if (!_isActive) { return; }

        float spintAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spintAddAmount, 0);
        _totalSpinAmount += spintAddAmount;
        if (_totalSpinAmount >= 360f) 
        {
            _isActive = false;
            _onActionComplete();
        }
        
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        var validGridPositionsList = new List<GridPosition>();
        GridPosition unitGridPosition = _unit.GridPosition;
        validGridPositionsList.Add(unitGridPosition);
        return validGridPositionsList;
    }
}
