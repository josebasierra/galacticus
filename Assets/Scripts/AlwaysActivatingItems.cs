using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysActivatingItems : MonoBehaviour
{
    Weapon[] weapons;
    Rewindable rewindable;

    // Start is called before the first frame update
    void Start()
    {
        weapons = GetComponentsInChildren<Weapon>();
        rewindable = GetComponentInChildren<Rewindable>();
    }


    void FixedUpdate()
    {
        bool itemState = true;
        if (rewindable != null && rewindable.IsRewinding()) itemState = false;

        foreach(var weapon in weapons)
        {
            weapon.Activate(itemState);
        }
    }
}
