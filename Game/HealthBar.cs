using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;

    private int maxHealth = 0;

    private int health = 0;

    private void Start()
    {
        health = transform.GetComponentInParent<Health>().GetMaxHealth();
        maxHealth = health;
    }

    public void Hit(int hitPoint)
    {
        slider.value =(float) (health-hitPoint) / maxHealth;
        health -= hitPoint;
    }

    public void SetHealth(int health)
    {
        this.health = health;

        slider.value = (float) health / maxHealth;
    }
}
