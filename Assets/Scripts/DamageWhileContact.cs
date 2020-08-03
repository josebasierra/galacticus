using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWhileContact : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnCollisionStay(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && !collision.gameObject.CompareTag(tag))
        {
            health.TakeDamage(damage);
        }
    }
}
