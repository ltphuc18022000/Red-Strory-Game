using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private bool isGrounded = false;
    private float groundCheckDistance = 2f;
    [SerializeField] private LayerMask groundMask;


    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckDistance, groundMask);

        if (!isGrounded)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 8.0f, Space.World);
        }
    }

    
}
