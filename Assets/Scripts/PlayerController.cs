using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainAttackObject;
    [SerializeField] GameObject secondaryAttackObject;
    [SerializeField] GameObject mainSkillObject;

    [SerializeField] float energyConsumptionPerSecond;

    IItem mainAttackItem;
    IItem secondaryAttackItem;
    IItem mainSkillItem;

    BasicMovement basicMovement;
    Health health;
    Energy energy;
    Rewindable rewindable;

    bool jump, mainAttack, secondaryAttack, mainSkill;
    Vector2 moveDirection;


    void Start()
    {
        basicMovement = GetComponent<BasicMovement>();
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        rewindable = GetComponent<Rewindable>();

        if (mainAttackObject != null) mainAttackItem = mainAttackObject.GetComponent<IItem>();
        if (secondaryAttackObject != null) secondaryAttackItem = secondaryAttackObject.GetComponent<IItem>();
        if (mainSkillObject != null) mainSkillItem = mainSkillObject.GetComponent<IItem>();

        jump = false;
        mainAttack = false;
        secondaryAttack = false;
        mainSkill = false;
        moveDirection = Vector2.zero;
    }


    void FixedUpdate()
    {
        if (rewindable.IsRewinding() || health.IsDead())
        {
            basicMovement.SetMoveDirection(Vector2.zero);
            basicMovement.SetJump(false);
            mainAttackItem.Activate(false);
            if (health.IsDead()) secondaryAttackItem.Activate(false);  //hacks everywhere.. fuck game jam time
        }
        else
        {
            AimAtMouse();

            basicMovement.SetJump(jump);
            basicMovement.SetMoveDirection(moveDirection);
            mainAttackItem.Activate(mainAttack);
            secondaryAttackItem.Activate(secondaryAttack);
        }

        mainSkillItem.Activate(mainSkill);
    }


    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }


    void OnJump(InputValue inputValue)
    {
        jump = inputValue.Get<float>() > 0;
    }


    void OnMainAttack(InputValue inputValue)
    {
        mainAttack = inputValue.Get<float>() > 0;
    }


    void OnSecondaryAttack(InputValue inputValue)
    {
        secondaryAttack = inputValue.Get<float>() > 0;
    }


    void OnMainSkill(InputValue inputValue)
    {
        mainSkill = inputValue.Get<float>() > 0;
    }


    void AimAtMouse()
    {
        var direction = GetAimPosition() - transform.position;
        direction = new Vector3(direction.x, 0, direction.z);

        transform.forward = direction;
    }


    Vector3 GetAimPosition()
    {
        var screenPoint = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(screenPoint);

        LayerMask mask = LayerMask.GetMask("Mouse");
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, mask))
        {
            return hit.point;
        }

        return new Vector3(0,0,-1);
    }
}
