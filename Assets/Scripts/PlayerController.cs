using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    void Start()
    {
        gameObject.AddComponent<PlayerInput>();    
    }


    public void OnMove(InputValue value)
    {
        GetComponent<BasicMovement>().SetMoveDirection(value.Get<Vector2>());
    }


    public void OnJump(InputValue value)
    {
        bool jumpValue = value.Get<float>() > 0;
        GetComponent<BasicMovement>().SetJump(jumpValue);
    }


    public void OnSelect(InputValue value)
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


    public void OnMainSkill(InputValue inputValue)
    {
        bool value = inputValue.Get<float>() > 0 ? true : false;
        GetComponent<AreaRewinder>().Activate(value);
    }
}
