using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLaser : Laser, IItem
{
    [SerializeField] float dps;

    protected override void HitEffect(RaycastHit hit)
    {
        var health = hit.transform.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(dps * Time.fixedDeltaTime);
        }
    }

}
