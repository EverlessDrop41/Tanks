using System;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float TankLerpSpeed = 1f;
    // public float Acceleration = 1f;
    public float MoveSpeed = 10f;

    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private float _horizontalMove;
    private float _verticalMove;

    void Update()
    {
        _horizontalMove = Input.GetAxis("Horizontal") * MoveSpeed;
        _verticalMove = Input.GetAxis("Vertical") * MoveSpeed;
    }

    void FixedUpdate()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 moveVector = (camTransform.right * _horizontalMove) + (camTransform.forward * _verticalMove);
        moveVector.y = 0;
        RotateTank(moveVector);

        RB.velocity = moveVector ;
    }

    private void RotateTank(Vector3 moveVector)
    {
        if (_horizontalMove != 0 || _verticalMove != 0)
        {
            float tankRotationAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;

            var tankRotation = Quaternion.Euler(transform.localEulerAngles.x, tankRotationAngle, transform.localEulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, tankRotation, TankLerpSpeed);
        }
    }
}
