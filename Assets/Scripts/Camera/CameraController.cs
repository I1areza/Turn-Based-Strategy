using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private float MoveSpeed = 10f;
    private float rotationSpeed = 100;
    private float zoomAmount =1f;
    private CinemachineTransposer _transposer;
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    private Vector3 _targetFollowOffset;

    [SerializeField] CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField]  private float _zoomSpeed = 5f;


    private void Start()
    {
         _transposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _transposer.m_FollowOffset;
    }
    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();






    }


    private void HandleRotation() 
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y += 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y += -1f;
        }

        transform.eulerAngles += rotationVector * Time.deltaTime * rotationSpeed;
    }
    private void HandleMovement() 
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z += -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x += -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x += 1f;
        }

        Vector3 moveVector = Vector3.Normalize(transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x);
        transform.position += moveVector * MoveSpeed * Time.deltaTime;
    }

    private void HandleZoom() 
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y += zoomAmount;
        }
        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        _transposer.m_FollowOffset = Vector3.Lerp(_transposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * _zoomSpeed);

    }
}
