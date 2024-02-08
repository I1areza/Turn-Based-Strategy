using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class UnitControlSystem : MonoBehaviour
{

    [SerializeField] private Unit _selectedUnit;
    [SerializeField] LayerMask _unitLayerMast;
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
                _selectedUnit = unit;
                return true;
            }  
        }
        return false;
    }
}
