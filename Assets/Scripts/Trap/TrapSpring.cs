using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpring : MonoBehaviour
{
    public float jumpHeight = 30f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerMovement>().CustomJump(jumpHeight);
    }
}
