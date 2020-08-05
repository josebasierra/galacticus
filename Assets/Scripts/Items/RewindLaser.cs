﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindLaser : Laser, IItem
{
    protected override void HitEffect(RaycastHit hit)
    {
        var rewindable = hit.transform.GetComponent<Rewindable>();
        if (rewindable != null)
        {
            rewindable.StartRewind(0.1f);
        }
    }

}