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
}
