using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 _targetPosition;
    private float _stoppingDistance = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, _targetPosition)>_stoppingDistance)
        {
Vector3 moveDirection = (_targetPosition - transform.position).normalized;
        float moveSpeed = 4f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.T))
        {
            Move(new Vector3(4,0,4));
        }
        }
        
    }

    private void Move(Vector3 targetPosition)
    {
        this._targetPosition = targetPosition;
    }
}
