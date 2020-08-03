using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool destroyOnContact = true;

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        if (destroyOnContact)
        {
            GameManager.Instance().CustomDestroy(this.gameObject);
        }
    }
}
