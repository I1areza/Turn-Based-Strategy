using System;
using UnityEngine;

public class UnitControlSystem : MonoBehaviour
{
    public static UnitControlSystem Instance;
    public event EventHandler OnUnitSelected;
    
    
    [SerializeField] private Unit _selectedUnit;
    [SerializeField] LayerMask _unitLayerMast;
    
    

    public Unit SelectedUnit
    {
        private set { _selectedUnit = value; }
        get { return _selectedUnit; }
    }

   


    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There's more than one instance of UnitControlSyste: "+transform + " - " + Instance );
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(HandleUnitSelection())return;
            
            _selectedUnit?.Move(MouseWorld.GetPosition());
        }
    }

    public bool HandleUnitSelection() 
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayerMast))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }  
        }
        return false;
    }

    public void SetSelectedUnit(Unit unit) 
    {
        _selectedUnit = unit;
        OnUnitSelected?.Invoke(this, EventArgs.Empty);
    }
}
