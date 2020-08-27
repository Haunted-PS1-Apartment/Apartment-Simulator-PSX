using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10; //meters per second
    [SerializeField] float rotationSpeed = 10; //degrees per second
    [SerializeField] Animator kristyAnimator = null;
    [SerializeField] float dialogRotateTime = .25f;

    Rigidbody body;
    PlayerInputActions controls;

    bool stopped = false;
    PlayerAnim animationState = PlayerAnim.None;

    float rotateIncrement = 0;
    float rotateTarget = 0;
    float rotatePrevious = 0;

    public enum PlayerAnim
    {
        None,
        Idle,
        Talk0,
        Talk1,
        Think,
        Thank,
        Push
    }

    public void StartAnimation(PlayerAnim animState, Transform target = null)
    {
        if(target != null)
        {
            rotatePrevious = transform.rotation.eulerAngles.y;
            rotateTarget = Quaternion.LookRotation(Vector3.up, transform.position - target.position).eulerAngles.y;
            rotateIncrement = 0;
        } else
        {
            rotatePrevious = transform.rotation.eulerAngles.y;
            rotateTarget = rotatePrevious;
        }

        stopped = true;
        if(animationState == animState)
        {
            return;
        }
        animationState = animState;
        switch(animState)
        {
            case PlayerAnim.None:
                kristyAnimator.SetBool("Walk", false);
                break;
            case PlayerAnim.Idle:
                kristyAnimator.SetTrigger("ForceIdle");
                break;
            default:
                kristyAnimator.SetTrigger(animState.ToString());
                break;
        }
    }

    public void EndAnimation()
    {
        stopped = false;
        animationState = PlayerAnim.None;
        kristyAnimator.SetTrigger("ForceIdle");
    }

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
        if (!stopped)
        {
            //normal movement
            Vector2 movement = controls.Player.Move.ReadValue<Vector2>();

            if (movement.y > 0)
            {
                body.velocity = transform.forward * speed; //go forward
                kristyAnimator.SetBool("Walk", true);
                kristyAnimator.SetBool("TurnLeft", false);
                kristyAnimator.SetBool("TurnRight", false);
            }
            else if (movement.y < 0)
            {
                body.velocity = -transform.forward * speed; //go backward
                kristyAnimator.SetBool("Backwards", true);
                kristyAnimator.SetBool("TurnLeft", false);
                kristyAnimator.SetBool("TurnRight", false);
            }
            else
            {
                body.velocity = Vector3.zero; //stop.
                kristyAnimator.SetBool("Walk", false);
                kristyAnimator.SetBool("Backwards", false);
                if (movement.x < 0)
                {
                    kristyAnimator.SetBool("TurnLeft", true);
                    kristyAnimator.SetBool("TurnRight", false);
                }
                else if (movement.x > 0)
                {
                    kristyAnimator.SetBool("TurnLeft", false);
                    kristyAnimator.SetBool("TurnRight", true);
                } 
                else
                {
                    kristyAnimator.SetBool("TurnLeft", false);
                    kristyAnimator.SetBool("TurnRight", false);
                }
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
        else
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            rotateIncrement = Mathf.Min(rotateIncrement + Time.fixedDeltaTime, dialogRotateTime);
            transform.rotation = Quaternion.Euler(0, Mathf.LerpAngle(rotatePrevious, rotateTarget, rotateIncrement / dialogRotateTime), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
