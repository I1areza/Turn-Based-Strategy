using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    [SerializeField] GameObject _busyPlaque;
    // Start is called before the first frame update
    void Start()
    {
        UnitActionSystem.Instance.OnSelectedBusyChanged += UnitActionSystem_OnSelectedBusyChanged;
        _busyPlaque.SetActive(false);
    }

    private void UnitActionSystem_OnSelectedBusyChanged(object sender, ActionBusyEventArgs e)
    {
        _busyPlaque.SetActive(e.BusyState);
    }
}
