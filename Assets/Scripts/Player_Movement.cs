using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public float _speed = 1f;

    private Rigidbody2D _rigidbody;

    Vector2 dirInput = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void GetVector(InputAction.CallbackContext context)
    {
        dirInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _speed * dirInput * Time.fixedDeltaTime;
    }


}
