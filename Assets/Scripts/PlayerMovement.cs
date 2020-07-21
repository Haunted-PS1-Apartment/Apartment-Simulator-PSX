using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10; //meters per second
    [SerializeField] float rotationSpeed = 10; //degrees per second

    Rigidbody body;
    PlayerInputActions controls;

    // Do stuff when the game starts
    void Awake()
    {
        body = GetComponent<Rigidbody>();
        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // FixedUpdate is called once per physics step
    private void FixedUpdate()
    {
        Vector2 movement = controls.Player.Move.ReadValue<Vector2>();

        if(movement.y > 0)
        {
            body.velocity = transform.forward * speed; //go forward
        } 
        else if (movement.y < 0)
        {
            body.velocity = -transform.forward * speed; //go backward
        } else
        {
            body.velocity = Vector3.zero; //stop.
        }

        if (movement.x < 0)
        {
            body.angularVelocity = -transform.up * rotationSpeed * Mathf.Deg2Rad; //rotate left
        }
        else if (movement.x > 0)
        {
            body.angularVelocity = transform.up * rotationSpeed * Mathf.Deg2Rad; //rotate right
        }
        else
        {
            body.angularVelocity = Vector3.zero; //stop.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
