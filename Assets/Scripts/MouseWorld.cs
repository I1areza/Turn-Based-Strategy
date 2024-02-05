using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField]
    private LayerMask _mask;


    private static MouseWorld _instance;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


  

    public static Vector3 GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _instance._mask);
        return raycastHit.point;
    }
}
