using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private CollectablesConfig collectablesConfig = null;

    private int hitPoint;
    private bool flag = true;

    private Transform target = null;

    private void Start()
    {
        Health health = transform.GetComponentInParent<Health>();
        if (health == null)
            return;

        hitPoint = health.GetHitPoint();
    }

    public void SetTarget(Transform target)
    {
        if (this.target != target)
            flag = true;

        this.target = target;
    }

    private void DeathAnimationPlaybackFinished()
    {
        string tag = transform.parent.tag;

        Destroy(transform.parent.gameObject);

        if(tag.Equals("Player"))
        {
            FindObjectOfType<GameManager>().StartAgain();
        }
    }

    private void ExplosionAnimationPlaybackFinished()
    {
        Destroy(transform.parent.gameObject);
    }

    public void DropCollectables()
    {
        if(collectablesConfig == null)
        {
            return;
        }

        int count = (int)Random.Range(2f, 5f);

        for (int i = 0; i < count; i++)
        {
            Instantiate(collectablesConfig.GetCollectables(), transform.position, Quaternion.identity);
        }
    }

    private void Hit()
    {
        if(target != null)
        {
            if (flag)
            {
                target.GetComponentInChildren<Animator>().SetTrigger("Hurting");
            }

            if (target.GetComponent<Health>().GetHealth() <= 0)
            {
                flag = false;
            }

            target.GetComponentInChildren<HealthBar>().Hit(hitPoint);
        }
    }
}
