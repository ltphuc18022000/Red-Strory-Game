using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBasic : MonoBehaviour
{
    public float damage = 1.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().TakeDamage(damage);

            if (other.gameObject.GetComponent<PlayerMovement>().enabled)
                other.gameObject.GetComponent<PlayerMovement>().Jump();
        }
    }
}
