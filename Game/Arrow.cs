using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Arrow Option")]
    [SerializeField]
    private float arrowSpeed = 0f;
    [SerializeField]
    private int hitPoint = 0;

    public void Fire(int direction)
    {
        transform.localScale = new Vector2(direction, 1);
        Vector2 velocity = new Vector2(arrowSpeed * direction, 0);
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInChildren<Animator>().SetTrigger("Hurting");
            collision.GetComponent<Health>().Hit(hitPoint);
            collision.GetComponentInChildren<HealthBar>().Hit(hitPoint);
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        Destroy(gameObject);
    }
}
