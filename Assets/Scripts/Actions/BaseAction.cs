using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    protected Unit _unit;
    protected bool _isActive;
    // Start is called before the first frame update
    
    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
    }

}
