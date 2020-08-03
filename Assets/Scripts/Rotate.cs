using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rotate : MonoBehaviour
{
    [SerializeField] Vector3 angularVelocity = Vector3.zero;
    Rigidbody myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        myRigidbody.angularVelocity = angularVelocity;
    }
}
