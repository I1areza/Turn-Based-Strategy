using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float MoveSpeed = 10f;
    private float rotationSpeed = 100;

    // Update is called once per frame
    void Update()
    {
      
        Vector3 inputMoveDir = new Vector3(0,0,0);

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
        Debug.Log(moveVector);
        transform.position += moveVector * MoveSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0,0,0);
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
}
