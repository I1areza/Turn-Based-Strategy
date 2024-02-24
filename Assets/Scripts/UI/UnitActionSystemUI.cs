
using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform _actionPrefab;
    [SerializeField] private Transform _actionContainer;

    void Start()
    {
        UnitActionSystem.Instance.OnUnitSelected += UnitActionSystem_OnSelectedUnitChanged;
        CreateUnitActionButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUnitActionButtons() 
    {
        foreach(Transform actionButton in _actionContainer) 
        {
            Destroy(actionButton.gameObject);
        }
        var unit = UnitActionSystem.Instance.SelectedUnit;
        foreach(var action in unit.Actions) 
        {
            var actionButton = Instantiate(_actionPrefab, _actionContainer);
            actionButton.GetComponent<ActionButtonUI>().SetBaseAction(action);
        }

    }
    
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e) 
    {
        CreateUnitActionButtons();
    }
}
