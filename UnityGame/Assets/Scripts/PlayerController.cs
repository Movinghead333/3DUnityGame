using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 2f;
    public float turnSpeed = 2f;

    // animation flags
    public bool isWalking = false;
    public bool isJumping = false;
    public bool lightAttacking = false;
    private bool nextLightAttackPossible = true;

    public Animator animator;

    private Vector2 direction = new Vector2(0, 1);
    private CharacterController controller;

    public Vector3 characterVelocity;
    private float gravity = 9.81f * 2f;
    private float jumpSpeed = 8;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Jumping
        controller.Move(characterVelocity * Time.deltaTime);
        if (!controller.isGrounded)
        {
            characterVelocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            characterVelocity.y = -0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            animator.SetTrigger("jump");
            //Debug.Log("jump");
        }

        animator.SetBool("jumping", !controller.isGrounded);
        isJumping = !controller.isGrounded;

        //Debug.Log("Grounded: " + controller.isGrounded);
        #endregion

        #region Turning
        if (Input.GetKey(KeyCode.Q))
        {
            Vector2 left = new Vector2(-direction.y, direction.x);
            direction = (direction + Time.deltaTime * left * turnSpeed).normalized;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Vector2 right = new Vector2(direction.y, -direction.x);
            direction = (direction + Time.deltaTime * right * turnSpeed).normalized;
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
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

        if (Input.GetKey(KeyCode.W))
        {
            movement = runSpeed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = -runSpeed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

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
}
