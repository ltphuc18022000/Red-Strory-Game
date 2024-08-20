using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;

    private float speed;
    private float distance;
    private float direction;

    private Transform target;

    void Start()
    {
        anim = GetComponent<Animator>();
        direction = 1f;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            distance = transform.position.x - target.position.x;
            if (distance > 1)
            {
                direction = 1f;
                transform.localScale = new Vector3(-1, 1, 1);
            }    
            else
            {
                direction = -1f;
                transform.localScale = Vector3.one;
            }    

            Vector2 velocity = new Vector2(direction * speed, transform.position.y);
            GetComponent<Rigidbody2D>().velocity = -velocity;

        }
    }

    public void SetTarget(Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
        anim.SetFloat("speed", speed);
    }

    public void Stop()
    {
        this.speed = 0;
        anim.SetFloat("speed", speed);
    }
}
