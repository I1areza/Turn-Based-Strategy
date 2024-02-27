using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance;
    public event EventHandler OnUnitSelected;


    [SerializeField] private Unit _selectedUnit;
   
    [SerializeField] LayerMask _unitLayerMast;
    private bool _isBusy;
    private BaseAction _selectedAction;

    public Unit SelectedUnit
    {
        private set { _selectedUnit = value; }
        get { return _selectedUnit; }
    }




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
        OnUnitSelected?.Invoke(this, EventArgs.Empty);
    }

    private void SetBusy() 
    {
        _isBusy = true;
    }

    private void ClearBusy() 
    {
        _isBusy = false;
    }

    public void SetSelectedAction(BaseAction action) 
    {
        _selectedAction = action;
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if(_selectedAction.IsValidActionGridPosition(mouseGridPosition)) 
            {
                SetBusy();
                _selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            }
        }
    }
}
