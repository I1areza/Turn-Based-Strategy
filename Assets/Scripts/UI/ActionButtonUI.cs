using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private Button _button;
    private BaseAction _action;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }


    public void SetBaseAction(BaseAction action) 
    {
        _textMeshPro.text = action.GetActionName();
        _action= action;
        _button.onClick.AddListener(() => 
        {
            UnitActionSystem.Instance.SetSelectedAction(_action);
        });
    }
}
