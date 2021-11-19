using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 0;
    [SerializeField]
    private int hitPoint = 0;

    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Hit(int hitPoint)
    {
        health -= hitPoint;

        if(health <= 0)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                transform.GetComponent<EnemyController>().SetIsActive(false);
            }
            else
            {
                transform.GetComponent<PlayerController>().SetIsActive(false);
            }
        }
    }

    public void IncreaseHealth(int healthPoint)
    {
        if (healthPoint == maxHealth)
            health = maxHealth;
        else
            health += healthPoint;

        if (health >= maxHealth)
            health = maxHealth;

        GetComponentInChildren<HealthBar>().SetHealth(health);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetHitPoint()
    {
        return hitPoint;
    }
}
