using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance;
    public event EventHandler OnUnitSelectedChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<ActionBusyEventArgs> OnSelectedBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit _selectedUnit;
    [SerializeField] LayerMask _unitLayerMast;
    private bool _isBusy;
    private BaseAction _selectedAction;

    public Unit SelectedUnit
    {
        private set { _selectedUnit = value; }
        get { return _selectedUnit; }
    }
    public BaseAction SelectedAction { get { return _selectedAction; } }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one instance of UnitControlSyste: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(_selectedUnit);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBusy) { return; }
        if(EventSystem.current.IsPointerOverGameObject()) { return; }
        if (TryHandleUnitSelection())
        {
            return;
        }
        HandleSelectedAction();
        
    }

    public bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) { 
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayerMast))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if(unit == _selectedUnit) 
                    {
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }
        return false;
    }

    public void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        SetSelectedAction(_selectedUnit.MoveAction);
        OnUnitSelectedChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetBusy()
    {
        _isBusy = true;
        var eventArgs = new ActionBusyEventArgs();
        eventArgs.BusyState = _isBusy;
        OnSelectedBusyChanged(this, eventArgs);
    }

    private void ClearBusy()
    {
        _isBusy = false;
        var eventArgs = new ActionBusyEventArgs();
        eventArgs.BusyState = _isBusy;
        OnSelectedBusyChanged(this, eventArgs);
    }

    public void SetSelectedAction(BaseAction action) 
    {
        _selectedAction = action;
        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(_selectedAction.IsValidActionGridPosition(mouseGridPosition)) 
            {
                if (_selectedUnit.TrySpendActionPoints(SelectedAction)) 
                {
                    OnActionStarted(this, EventArgs.Empty);
                    SetBusy();
                    _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                }
            }
        }
    }
}
