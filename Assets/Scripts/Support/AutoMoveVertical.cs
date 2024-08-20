using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveVertical : MonoBehaviour
{
    public float movementDistance;
    public float speed;
    public bool movingUp;

    private float topPoint;
    private float bottomPoint;

    void Start()
    {
        topPoint = transform.position.y + movementDistance;
        bottomPoint = transform.position.y - movementDistance;
    }

    void Update()
    {
        // move up
        if (movingUp)
        {
            if (transform.position.y < topPoint)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            }
            else
                movingUp = false;
        }
        // move down
        else
        {
            if (transform.position.y > bottomPoint)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            }
            else
                movingUp = true;
        }
    }
}
