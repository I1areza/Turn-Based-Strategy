using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class MoveAction : BaseAction
{
    #region Variables
    private Vector3 _targetPosition;
    private float _stoppingDistance = .1f;
    private float moveSpeed = 4f;
    private float rotationSpeed = 5f;

    [SerializeField] private int maxMoveDistance;
    [SerializeField] private Animator unitAnimator;
    #endregion

    #region Built-in Methods
    protected override void Awake()
    {
        base.Awake();
        _actionCost = 1;
        _targetPosition = transform.position;
    }

    void Start()
    {
        _unit = GetComponent<Unit>();
    }

    void Update()
    {
        if(!_isActive) { return; }
        if (Vector3.Distance(transform.position, _targetPosition) > _stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            unitAnimator.SetBool("IsWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            _isActive = false;
            _onActionComplete();
        }
    }
    #endregion

    public override void TakeAction(GridPosition gridPosition, Action OnActionComplete)
    {
        _onActionComplete = OnActionComplete;
        _isActive = true;
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public override List<GridPosition> GetValidActionGridPositionList() 
    {
        var validGridPositionsList = new List<GridPosition>();
        for(int x = -maxMoveDistance; x<=maxMoveDistance; x++) 
        {
            for(int z = -maxMoveDistance; z<=maxMoveDistance; z++) 
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = _unit.GridPosition + offsetGridPosition;
                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) 
                {
                    continue;
                }
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) 
                {
                    continue;
                }
                validGridPositionsList.Add(testGridPosition);
            }
        }
        return validGridPositionsList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
