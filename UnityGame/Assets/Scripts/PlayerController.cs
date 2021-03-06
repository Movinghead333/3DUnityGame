﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 2f;

    private float rollingSpeed = 6.5f;

    // animation flags
    public bool isWalking = false;
    public bool isJumping = false;
    public bool lightAttacking = false;
    private bool nextLightAttackPossible = true;
    private bool rolling = false;

    public Animator animator;

    private Vector2 direction = new Vector2(0, 1);
    private CharacterController controller;

    public Vector3 characterVelocity;
    private float gravity = 9.81f * 2f;
    private float jumpSpeed = 8;

    // rotation
    private float turnSpeed = 25;


    Vector3 facingDirection = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Gravity
        controller.Move(characterVelocity * Time.deltaTime);
        if (!controller.isGrounded)
        {
            characterVelocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            characterVelocity.y = -0.5f;
        }
        #endregion

        #region Rolling

        if (rolling)
        {
            controller.Move(facingDirection.normalized * Time.deltaTime * rollingSpeed);
            return;
        }

        #endregion

        #region Jumping

        if (Input.GetKeyDown(KeyCode.J) && controller.isGrounded)
        {
            animator.SetTrigger("jump");
            //Debug.Log("jump");
        }

        animator.SetBool("jumping", !controller.isGrounded);
        isJumping = !controller.isGrounded;

        //Debug.Log("Grounded: " + controller.isGrounded);
        #endregion

        #region Turning

        // update directions
        direction = new Vector2(DSCameraController.lookDir.x, DSCameraController.lookDir.z);
        //transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));

        #endregion

        #region Attacking
        if (Input.GetKeyDown(KeyCode.Mouse0) && controller.isGrounded)
        {
            Debug.Log("Attack!");
            if (nextLightAttackPossible)
            {
                isWalking = false;
                animator.SetBool("walking", isWalking);
                lightAttacking = true;
                animator.SetBool("lightAttacking", lightAttacking);
                return;
            }
        }

        if (!nextLightAttackPossible)
        {
            return;
        }
        #endregion

        #region MovementInPlain
        Vector3 movement = Vector3.zero;

        isWalking = false;

        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(direction.x, 0, direction.y);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement -= new Vector3(direction.x, 0, direction.y);
            isWalking = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= new Vector3(direction.y, 0, -direction.x);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(direction.y, 0, -direction.x);
            isWalking = true;
        }

        movement.Normalize();
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement, Vector3.up),
                Time.deltaTime * turnSpeed);

            facingDirection = transform.forward;


            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                rolling = true;
                animator.SetBool("rolling", rolling);
                return;
            }
        }

        movement = runSpeed * Time.deltaTime * movement.normalized;

        controller.Move(movement);
        animator.SetBool("walking", isWalking);
        #endregion
    }

    public void StartJumpMovement()
    {
        characterVelocity.y = jumpSpeed;
        Debug.Log("start jump movent");
    }

    public void PlayerLanded()
    {
        Debug.Log("player landed");
    }

    public void ResetLightAttackCooldown()
    {
        nextLightAttackPossible = true;
        animator.SetBool("lightAttackReady", nextLightAttackPossible);
        lightAttacking = false;
        animator.SetBool("lightAttacking", lightAttacking);
    }

    public void SetLightAttackCooldown()
    {
        nextLightAttackPossible = false;
        animator.SetBool("lightAttackReady", nextLightAttackPossible);
    }

    public void OnRollingFinished()
    {
        rolling = false;
        animator.SetBool("rolling", rolling);
    }
}
