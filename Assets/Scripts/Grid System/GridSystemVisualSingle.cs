using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    public GridPosition GridPosition { private set; get; }

    public void Show() 
    {
        _renderer.enabled = true;
    }

    public void Hide() 
    {
        _renderer.enabled=false;
    }

    public void SetGridPosition(GridPosition gridPosition)  
    {
        GridPosition = gridPosition;
    }
}
