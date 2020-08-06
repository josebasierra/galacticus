using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float timeToAchieveSpeed;

    [SerializeField] float airManeuverForce; 
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;

    Rigidbody myRigidbody;
    Collider myCollider;
    ContactPoint[] contactPoints;

    Vector3 moveDirection = Vector2.zero;

    bool jump = false;
    bool isJumpOnCooldown = false;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        myRigidbody.sleepThreshold = 0;
        contactPoints = new ContactPoint[5];
    }


    void FixedUpdate()
    {
        bool isOnGround = PhysicsUtility.IsOnGround(myCollider);

        if (isOnGround)
        {
            // Movement
            var currentVelocity = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);
            var desiredVelocity = moveDirection * moveSpeed;

            var force = PhysicsUtility.ComputeRequiredForce(currentVelocity, desiredVelocity, timeToAchieveSpeed, myRigidbody.mass);
            myRigidbody.AddForce(force);

            //TODO: Dont slow down player 
            //if (Vector3.Dot(force, desiredVelocity) > 0)
            //{
            //    myRigidbody.AddForce(force);
            //}

            // Jump
            if (jump && !isJumpOnCooldown && isOnGround)
            {
                myRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                isJumpOnCooldown = true;
                Invoke(nameof(EnableJump), jumpCooldown);
            }
        }
        else
        {
            //air maneuvers
            myRigidbody.AddForce(moveDirection * airManeuverForce);
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


    private void OnCollisionStay(Collision collision)
    {
        if (collision.contactCount > 5) return;

        int n = collision.GetContacts(contactPoints);

        Debug.Log("player colliding");
    }

}
