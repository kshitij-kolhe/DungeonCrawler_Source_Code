using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private int healthPoint = 0;

    private Transform target;

    private int count = 0;
    private bool groundHit;

    private void Awake()
    {
        if(healthPoint == 0)
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.8f, 0.8f), 0), ForceMode2D.Impulse);
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if(target != null && groundHit)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position,15 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().IncreaseHealth(healthPoint);

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        count++;

        if (count == 3)
        {
            groundHit = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
