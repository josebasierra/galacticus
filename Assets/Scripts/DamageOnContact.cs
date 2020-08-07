using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool destroyOnContact = true;
    [SerializeField] GameObject particleEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null && !collision.gameObject.CompareTag(tag))
        {
            health.TakeDamage(damage);
        }

        if (destroyOnContact)
        {
            GameManager.Instance().CustomDestroy(this.gameObject);
            if (particleEffect != null)
            {
                var effect = Instantiate(particleEffect);
                effect.transform.position = transform.position;
            }
        }
    }
}
