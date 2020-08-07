using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetween : MonoBehaviour
{

    [SerializeField] float speed = 2f;
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;

    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = (object2.transform.position - object1.transform.position).normalized * speed;
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = (myRigidbody.velocity).normalized * speed;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == object1) 
        {
            myRigidbody.velocity = (object2.transform.position - object1.transform.position).normalized * speed;
        }
        else if (other.gameObject == object2)
        {
            myRigidbody.velocity = (object1.transform.position - object2.transform.position).normalized * speed;
        }
    }
}
