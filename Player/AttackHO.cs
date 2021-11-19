using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHO : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 0f;
    [SerializeField]
    private LayerMask layerMask;

    private float timeBetweenAttack = 0.2f;
    private float timeSinceLastAttack = 0f;
    private bool isActive;
    private int hitPoint;

    void Start()
    {
        hitPoint = GetComponent<Health>().GetHitPoint();
        isActive = true;       
    }

    
    void Update()
    {
        if(isActive)
        {
            timeSinceLastAttack += Time.deltaTime;

            Attack();
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && timeSinceLastAttack > timeBetweenAttack)
        {
            //Attack
            Collider2D enemy= Physics2D.OverlapCircle(transform.position, attackRange, layerMask);

            GetComponentInChildren<Animator>().SetBool("Attacking", true);

            if (enemy != null && enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<Health>().Hit(hitPoint);
                GetComponentInChildren<AnimationController>().SetTarget(enemy.transform);
            }
            else if(enemy != null && enemy.CompareTag("Interactables"))
            {
                enemy.GetComponentInChildren<Animator>().SetBool("Exploding", true);
            }

            timeSinceLastAttack = 0;
        }
        else
        {
            //Do not Attack
            GetComponentInChildren<Animator>().SetBool("Attacking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void SetIsActive(bool isActive)
    {
        this.isActive = isActive;
    }
}
