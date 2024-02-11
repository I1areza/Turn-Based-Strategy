using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject _debugGridObject;
    private GridSystem _grid;
    private void Start()
    {
        _grid = new GridSystem(10, 10, 2, Vector3.zero);
        _grid.CreateDebugObjects(_debugGridObject);
    }
    private void Update()
    {
        //Debug.Log(_grid.GetGridPosition(MouseWorld.GetPosition()));
    }

}
