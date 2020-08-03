using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Change it to 'Global' rewinder
public class GlobalRewinder : MonoBehaviour, IItem
{
    bool isActivated = false;


    void FixedUpdate()
    {
        if (isActivated)
        {
            foreach (var rewindable in Rewindable.GetInstances())
            {
                rewindable.StartRewind(0.1f);
            }
        }
    }


    public void Activate(bool value)
    {
        isActivated = value;
    }


    //void RewindArea()
    //{
    //    var colliders = Physics.OverlapSphere(transform.position, radius);

    //    foreach (var collider in colliders)
    //    {
    //        var rewindable = collider.GetComponent<Rewindable>();
    //        if (rewindable != null)
    //        {
    //            rewindable.StartRewind(1.5f);
    //        }
    //    }
    //}


    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
