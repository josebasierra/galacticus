using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChaseTarget : MonoBehaviour
{
    [SerializeField] float chaseRadius = 20;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] Transform targetTransform;

    Rigidbody myRigidbody;

    void Start()
    {
        var target = GameObject.FindGameObjectWithTag("Player");
        if (target != null && targetTransform == null) targetTransform = target.transform;

        myRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (targetTransform == null) return;

        if (Vector3.Distance(transform.position, targetTransform.position) < chaseRadius)
        {
            var desiredVelocity = (targetTransform.position - transform.position).normalized * maxSpeed;
            var steeringForce = desiredVelocity - myRigidbody.velocity;

            myRigidbody.AddForce(steeringForce);
        }

    }
}
