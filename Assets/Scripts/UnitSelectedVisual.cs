using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    private void Start()
    {
        UnitControlSystem.Instance.OnUnitSelected += OnUnitSelectedChange;
        UpdateHalo();
    }

    private void OnUnitSelectedChange(object sender, EventArgs e)
    {
        UpdateHalo();
    }

    private void UpdateHalo() 
    {
        if (UnitControlSystem.Instance.SelectedUnit == _unit)
        {
            _meshRenderer.enabled = true;
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}