using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveHorizontal : MonoBehaviour
{
    public float movementDistance;
    public float speed;
    public bool originFlipLeft = true;

    public bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    void Start()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    void Update()
    {
        // move left
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        // move right
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }
}
