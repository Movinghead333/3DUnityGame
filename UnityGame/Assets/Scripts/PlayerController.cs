using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 2f;
    public float turnSpeed = 2f;

    public bool isWalking = false;
    public bool isJumping = false;
    public Animator animator;

    private Vector2 direction = new Vector2(0, 1);
    private CharacterController controller;

    private Vector3 characterVelocity;
    private float gravity = 9.81f;
    private float jumpSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

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

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement = runSpeed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
            isWalking = true;
            animator.speed = runSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement = -runSpeed * Time.deltaTime * new Vector3(direction.x, 0, direction.y);
            isWalking = true;
            animator.speed = runSpeed;
        }
        else
        {
            isWalking = false;
            animator.speed = 1f;
        }

        controller.Move(movement);


        if (!controller.isGrounded)
        {
            characterVelocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            characterVelocity.y = -0.05f;
        }

        controller.Move(characterVelocity * Time.deltaTime);

        Debug.Log("is Grounded: " + controller.isGrounded);

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            Debug.Log("jump");
            characterVelocity.y = jumpSpeed;
        }


        animator.SetBool("walking", isWalking);
        //animator.SetBool("isJumping", !controller.isGrounded);
    }
}
