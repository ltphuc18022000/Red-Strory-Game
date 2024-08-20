using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Flip : MonoBehaviour
{

    public Transform PlayerLocation;
    private bool isFlip = false;
    void Start()
    {
        //PlayerLocation = Get
    }
    public void FindPlayer()
    {
        Vector3 flip = transform.localScale;
        flip.z *= -1f;
        if (transform.position.x > PlayerLocation.position.x && isFlip)
        {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            isFlip = false;
        }
        else if (transform.position.x < PlayerLocation.position.x && !isFlip)
        {
            transform.localScale = flip;
            transform.Rotate(0f, 180f, 0f);
            isFlip = true;
        }

    }
 


    // Update is called once per frame
    void Update()
    {
        
    }
}
