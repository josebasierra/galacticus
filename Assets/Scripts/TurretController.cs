using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float detectionRadius = 20;
    
    Transform target;
    Health health;
    IItem item;

    Rewindable rewindable;


    void Start()
    {
        var playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null) target = playerObject.transform;

        health = GetComponent<Health>();
        item = GetComponentInChildren<IItem>();
        rewindable = GetComponent<Rewindable>();
    }

    void FixedUpdate()
    {
        if (target != null && !health.IsDead() && !rewindable.IsRewinding() && Vector3.Distance(transform.position, target.position) < detectionRadius)
        {
            var aimDirection = target.position - transform.position;
            transform.right = aimDirection;
            item.Activate(true);
        }
        else
            item.Activate(false);
    }
}
