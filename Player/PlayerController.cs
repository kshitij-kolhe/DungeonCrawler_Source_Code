using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    Animator animator;

    private bool isGrounded;
    private bool isActive;
    private bool canCrouch;
    private bool once = true;

    [Header("Player Options")]
    [SerializeField]
    private float runSpeed = 1f;
    [SerializeField]
    private float jumpForce = 1f;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator = transform.GetComponentInChildren<Animator>();
        AnimatorControllerParameter[] animatorControllerParameters = animator.parameters;
        
        foreach (AnimatorControllerParameter animatorControllerParameter in animatorControllerParameters)
        {
            if(animatorControllerParameter.name == "Crouching")
            {
                canCrouch = true;
            }
        }
        
        isActive = true;
    }

    void Update()
    {
        // Ground Check
        isGrounded = Physics2D.IsTouchingLayers(GetComponent<CircleCollider2D>(), LayerMask.GetMask("Foreground"));

        if (isActive)
        {
            Run();
            Jump();
        }
        else if(once)
        {
            Die();
        }
        
    }

    private void Run()
    {
        float horizontalControl = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalControl) > Mathf.Epsilon && isGrounded)
        {
            transform.localScale = new Vector2(Mathf.Sign(horizontalControl), 1);

            Vector2 playerVelocity = new Vector2(horizontalControl * runSpeed * Time.deltaTime, myRigidbody2D.velocity.y);
            myRigidbody2D.velocity = playerVelocity;
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 playerVelocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            myRigidbody2D.velocity = playerVelocity;
            isGrounded = false;
        }

        if (isGrounded)
        {
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Jumping", true);

            float horizontalControl = Input.GetAxis("Horizontal");
            if (Mathf.Abs(horizontalControl) > Mathf.Epsilon)
            {
                transform.localScale = new Vector2(Mathf.Sign(horizontalControl), 1);

                Vector2 playerVelocity = new Vector2(horizontalControl * runSpeed * Time.deltaTime, myRigidbody2D.velocity.y);
                myRigidbody2D.velocity = playerVelocity;
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Obstacles"))
        {
            isActive = false;
            once = false;
            Die2();
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Intermediate") && Input.GetKeyDown(KeyCode.W))
        {
            FindObjectOfType<GameManager>().Teleport();
        }
    }

    private void Die()
    {
        animator.SetBool("Dying", true);
        animator.SetBool("Jumping", false);
        animator.SetBool("Running", false);
        GetComponentInChildren<Canvas>().enabled = false;
        once = false;
        myRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    private void Die2()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Running", false);
        animator.SetTrigger("Hurting");
        animator.SetBool("Dying", true);
        GetComponentInChildren<Canvas>().enabled = false;
        myRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
        GetComponent<AttackHO>().SetIsActive(isActive);
    }

}
