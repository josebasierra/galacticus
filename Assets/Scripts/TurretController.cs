using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] float radius = 20;
    Transform target;
    Health health;
    Rewindable rewindable;
    IItem item;

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
        if (target != null && !health.IsDead() && !rewindable.IsRewinding() && Vector3.Distance(transform.position, target.position) < radius)
        {
            var aimDirection = target.position - transform.position;
            transform.right = aimDirection;
            item.Activate(true);
        }
        else
            item.Activate(false);
    }
}
