using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float _totalSpinAmount;
    
    public void Spin() 
    {
        _isActive = true;
        _totalSpinAmount = 0f;
    }
    
    void Update()
    {
        if (!_isActive) { return; }

        float spintAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spintAddAmount, 0);
        _totalSpinAmount += spintAddAmount;
        if (_totalSpinAmount >= 360f) 
        {
            _isActive = false;
        }
        
    }
}
