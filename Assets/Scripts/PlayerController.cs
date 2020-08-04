﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Weapon mainWeapon;
    [SerializeField] Weapon secondaryWeapon;
    [SerializeField] GlobalRewinder globalRewinder;

    BasicMovement basicMovement;


    Rewindable rewindable;

    void Start()
    {
        //gameObject.AddComponent<PlayerInput>();   
        rewindable = GetComponent<Rewindable>();
        basicMovement = GetComponent<BasicMovement>();
    }


    void FixedUpdate()
    {
        if (rewindable.IsRewinding())
        {
            //TODO: FIX
            basicMovement.SetMoveDirection(Vector2.zero);
            basicMovement.SetJump(false);
            mainWeapon.Activate(false);
            secondaryWeapon.Activate(false);
        }
        else
        {
            var mouseScreenPoint = Mouse.current.position.ReadValue();
            var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

            Vector2 lookDirection = (mouseScreenPoint - (Vector2)playerScreenPoint).normalized;

            transform.forward = new Vector3(lookDirection.x, 0, lookDirection.y);
        }
    }


    public void OnMove(InputValue value)
    {
        if (rewindable.IsRewinding())
        {
            return;
        }

        GetComponent<BasicMovement>().SetMoveDirection(value.Get<Vector2>());
    }


    public void OnJump(InputValue inputValue)
    {
        if (rewindable.IsRewinding())
        {
            return;
        }

        bool value = inputValue.Get<float>() > 0;
        GetComponent<BasicMovement>().SetJump(value);
    }


    public void OnMainAttack(InputValue inputValue)
    {
        if (rewindable.IsRewinding())
        {
            return;
        }

        bool value = inputValue.Get<float>() > 0;
        mainWeapon.Activate(value);
    }

    public void OnSecondaryAttack(InputValue inputValue)
    {
        //if (rewindable.IsRewinding())
        //{
        //    return;
        //}

        //bool value = inputValue.Get<float>() > 0;
        //secondaryWeapon.Activate(value);
    }


    public void OnMainSkill(InputValue inputValue)
    {
        bool value = inputValue.Get<float>() > 0;
        globalRewinder.Activate(value);
    }


    public void OnSecondarySkill()
    {
        RewindObjectUnderMouse();
    }


    void RewindObjectUnderMouse()
    {
        var screenPoint = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(screenPoint);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var hitObject = hit.transform.gameObject;
            var rewindable = hitObject.GetComponent<Rewindable>();

            if (rewindable != null)
            {
                rewindable.StartRewind(rewindable.GetMaximumRewindSeconds());
            }
        }
    }
}
