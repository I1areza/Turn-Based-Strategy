
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private ActionButtonUI _actionPrefab;
    [SerializeField] private Transform _actionContainer;
    [SerializeField] private TextMeshProUGUI _actionPointsText;
    private List<ActionButtonUI> _buttons;
    #endregion

    #region Built-in Methods
    private void Awake()
    {
        _buttons = new List<ActionButtonUI>();
    }
    void Start()
    {
        UnitActionSystem.Instance.OnUnitSelectedChanged += UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_onSelectedActionChanged;
        UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_onActionStarted;
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsText();
    }
    #endregion

    #region Event Methods
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e) 
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
        UpdateActionPointsText();
    }

    private void UnitActionSystem_onSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_onActionStarted(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }
    #endregion
    private void UpdateSelectedVisual()
    {
        foreach (var button in _buttons) 
        {
            button.UpdateSelectedVisual();
        }
    }

    private void CreateUnitActionButtons() 
    {
        foreach(Transform actionButton in _actionContainer) 
        {
            Destroy(actionButton.gameObject);
        }
        _buttons.Clear();
        var unit = UnitActionSystem.Instance.SelectedUnit;
        foreach(var action in unit.Actions) 
        {
            var actionButton = Instantiate(_actionPrefab, _actionContainer);
            _buttons.Add(actionButton);
            actionButton.SetBaseAction(action);
        }
    }

    private void UpdateActionPointsText() 
    {
        var actionPoints = UnitActionSystem.Instance.SelectedUnit.ActionPoints;
        _actionPointsText.text = $"Action Points: {actionPoints}";
    }
}
