using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField]
    private float detectionRange = 0;
    [SerializeField]
    private float attackRange = 0;

    [Header("Arrow Config")]
    [SerializeField]
    private GameObject Arrow = null;
    [SerializeField]
    private Transform firePos = null;

    private float timeBetweenAttack = 2f;
    private float timeSinceLastAttack = 0f;
    private bool canAttack = true;
    private bool isTouchingObstacle = false;
    private Vector2 spawnPos;
    private Transform target = null;
    private bool flag = true;

    void Start()
    {
        spawnPos = transform.position;
        GetComponentInChildren<AnimationController>().SetTarget(target);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        canAttack = true;
    }

    
    void Update()
    {
        canAttack = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), LayerMask.GetMask("Foreground"));
        isTouchingObstacle = Physics2D.IsTouchingLayers(GetComponent<BoxCollider2D>(), LayerMask.GetMask("Obstacles"));

        if (!canAttack)
        {
            flag = false;
        }

        if (target != null && GetComponent<EnemyController>().GetIsActive())
        {
            float X = Mathf.Abs(transform.position.x - target.position.x);
            float Y = Mathf.Abs(transform.position.y - target.position.y);

            if (isTouchingObstacle)
            {
                flag = false;
                GetComponent<EnemyController>().SetDirection((int)Mathf.Sign(spawnPos.x - transform.position.x));
            }
            else if (X < detectionRange && Y <= 1.1 && flag)
            {
                if (Vector2.Distance(transform.position, target.position) < attackRange)
                {
                    flag = true;
                }

                if (canAttack && flag)
                {
                    timeSinceLastAttack += Time.deltaTime;

                    if (Vector2.Distance(transform.position, target.position) < attackRange)
                    {
                        Attack();
                    }
                    else
                    {
                        //Move within attack range
                        GetComponent<EnemyController>().SetDirection((int)Mathf.Sign(target.position.x - transform.position.x));
                        GetComponentInChildren<Animator>().SetBool("Attacking", false);
                    }
                }
                else
                {
                    GetComponent<EnemyController>().SetDirection((int)Mathf.Sign(spawnPos.x - transform.position.x));
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, target.position) < attackRange)
                {
                    flag = true;
                }

                if ((int)transform.position.x == (int)spawnPos.x)
                {
                    //Stop at Spawn Position
                    GetComponent<EnemyController>().SetDirection(0);
                    flag = true;
                }
                else
                {
                    //Go back to Spawn Position
                    GetComponent<EnemyController>().SetDirection((int)Mathf.Sign(spawnPos.x - transform.position.x));
                }
            }
        }                
    }

    private void Attack()
    {
        LookAtPlayer();
        GetComponent<EnemyController>().SetDirection(0);

        if (timeSinceLastAttack > timeBetweenAttack)
        {
            GetComponentInChildren<Animator>().SetBool("Attacking", true);
            GameObject arrow = Instantiate(Arrow, firePos.position, Quaternion.identity) as GameObject;
            arrow.GetComponent<Arrow>().Fire((int)transform.localScale.x);
            timeSinceLastAttack = 0;
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("Attacking", false);
        }
    }

    private void LookAtPlayer()
    {
       if(transform.position.x - target.position.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
       else if(transform.position.x - target.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
