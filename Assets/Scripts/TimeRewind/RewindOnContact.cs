using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindOnContact : MonoBehaviour
{
    [SerializeField] float rewindSeconds = 0;
    [SerializeField] bool destroy = true;

    private void OnCollisionEnter(Collision collision)
    {
        var rewindable = collision.gameObject.GetComponent<Rewindable>();
        if (rewindable != null)
        {
            rewindable.StartRewind(rewindSeconds);
            GameManager.Instance().CustomDestroy(this.gameObject);
        }
    }
}
