using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D RB;

    [SerializeField]
    private float maxSpeed = 4, acceleration = 10, deacceleration = 20;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;

    public Vector2 MovementInput { get; set; }


    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MovementInput.magnitude > 0 && currentSpeed >= 0)
        {
            oldMovementInput = MovementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        RB.velocity = oldMovementInput * currentSpeed;
    }
}
