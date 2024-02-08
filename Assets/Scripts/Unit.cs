using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Vector3 _targetPosition;
    private float _stoppingDistance = .1f;
    private float moveSpeed = 4f;
    private float rotationSpeed = 5f;

    [SerializeField] private Animator unitAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        _targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, _targetPosition)>_stoppingDistance)
        {
            var moveDirection = (_targetPosition - transform.position).normalized;
            transform.position += moveSpeed * Time.deltaTime * moveDirection;
            unitAnimator.SetBool("IsWalking", true);
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
        
    }

    public void Move(Vector3 targetPosition)
    {
        this._targetPosition = targetPosition;
    }
}
