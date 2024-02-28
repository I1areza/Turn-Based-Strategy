
using System.Collections.Generic;
using UnityEngine;


public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] GridSystemVisualSingle _gridPrefab;
    private GridSystemVisualSingle[,] _gridSystemVisualSingles;
    public static GridSystemVisual Instance;

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


    // Start is called before the first frame update
    void Start()
    {
        _gridSystemVisualSingles = new GridSystemVisualSingle[
            LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()
            ];

        for (int x = 0; x< LevelGrid.Instance.GetWidth(); x++) 
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) 
            {
                var gridPosition = new GridPosition(x, z);
               
                var cell  = Instantiate<GridSystemVisualSingle>(_gridPrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                cell.Hide();
                cell.SetGridPosition(new GridPosition(x, z));
                _gridSystemVisualSingles[x, z] = cell;
            }
        }
    }

    private void Update()
    {
        UpdateValidGridSystemVisual();
    }

    public void UpdateValidGridSystemVisual() 
    {
        HideAllGridPositions();
        var action = UnitActionSystem.Instance.SelectedAction;
        var validPositions = action.GetValidActionGridPositionList();
        ShowAllGridPositions(validPositions);
    }

    public void HideAllGridPositions() 
    {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                _gridSystemVisualSingles[x, z].Hide();
            }
        }
    }

    public void ShowAllGridPositions(List<GridPosition> gridPositionList)
    {
        foreach(var position in gridPositionList) 
        {
            _gridSystemVisualSingles[position.x, position.z].Show();
        }
    }
}
