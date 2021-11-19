using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 0f;
    
    private bool isActive;
    private bool flag;
    private int direction = 0;

    private Rigidbody2D rigidBody = null;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        flag = true;
        isActive = true;
    }

    void Update()
    {
        if(isActive) // character is active in scene
        {
            //movement
            Move();
        }
        else if(!isActive && flag) // trigger only once for inactive character
        {
            //Death
            Die();
        }
    }

    private void Move()
    {
        switch (direction)
        {
            case -1:
                //Move Towards player
                transform.localScale = new Vector2(direction,1);
                GetComponentInChildren<Animator>().SetBool("Running", true);
                rigidBody.velocity = new Vector2(Time.deltaTime * movementSpeed * direction, 0f);
                break;

            case 0:
                //Do Nothing
                GetComponentInChildren<Animator>().SetBool("Running", false);
                rigidBody.velocity = new Vector2(0f, 0f);
                break;

            case 1:
                //Move back to Spawn Positon
                transform.localScale = new Vector2(direction, 1);
                GetComponentInChildren<Animator>().SetBool("Running", true);
                rigidBody.velocity = new Vector2(Time.deltaTime * movementSpeed * direction, 0f);
                break;

            default:
                break;
        }
    }

    private void Die() // character die sequence and cleanup
    {
        GetComponentInChildren<Animator>().SetBool("Dying", true);
        flag = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<Animator>().SetBool("Running", false);
        GetComponentInChildren<Canvas>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

}
