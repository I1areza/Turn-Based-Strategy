using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ActionButtonUI : MonoBehaviour
{
    #region Variables
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _selectedBorder;
    private Button _button;
    private BaseAction _action;
    private BaseAction _baseAction;
    #endregion

    #region Built-in Methods
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        
    }
    #endregion

    public void SetBaseAction(BaseAction action)
    {
        _baseAction = action;
        _textMeshPro.text = action.GetActionName();
        _action = action;
        _button.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.SetSelectedAction(_action);
        });
    }

    public void UpdateSelectedVisual() 
    {
        BaseAction selectedAction = UnitActionSystem.Instance.SelectedAction;
        _selectedBorder.SetActive(selectedAction == _baseAction);
    }
}
