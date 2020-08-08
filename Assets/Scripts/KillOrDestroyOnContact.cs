using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOrDestroyOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health != null)
        {
            Debug.Log("KillOrDestroyOnContact");
            health.TakeDamage(1000);
        }
        else
        {
            GameManager.Instance().CustomDestroy(other.gameObject);
        }
    }
}
