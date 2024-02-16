using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private GridSystemVisual _gridSystemVisual;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            _gridSystemVisual.UpdateValidGridSystemVisual();
        }
    }

}
