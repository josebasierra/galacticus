using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float timeToAchieveSpeed;

    [SerializeField] float jumpManeuverSpeed; 
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;


    Rigidbody myRigidbody;
    Collider myCollider;

    Vector3 moveDirection = Vector2.zero;

    bool jump = false;
    bool isJumpOnCooldown = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
    }


    void FixedUpdate()
    {
        bool isOnGround = PhysicsUtility.IsOnGround(myCollider);

        
            var targetSpeed = isOnGround ? moveSpeed : jumpManeuverSpeed;

            var currentVelocity = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);
            var desiredVelocity = moveDirection * targetSpeed;

            var force = PhysicsUtility.ComputeRequiredForce(currentVelocity, desiredVelocity, timeToAchieveSpeed, myRigidbody.mass);

            if (Vector3.Dot(force, desiredVelocity) > 0)
            {
                myRigidbody.AddForce(force);
            }


        if (jump && !isJumpOnCooldown && isOnGround)
        {
            myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isJumpOnCooldown = true;
            Invoke(nameof(EnableJump), jumpCooldown);
        }
    }


    public void SetMoveDirection(Vector2 moveDirection)
    {
        moveDirection = moveDirection.normalized;
        this.moveDirection.x = moveDirection.x;
        this.moveDirection.y = 0;
        this.moveDirection.z = moveDirection.y;
    }


    public void SetJump(bool value)
    {
        jump = value;
    }


    void EnableJump()
    {
        isJumpOnCooldown = false;
    }

}
