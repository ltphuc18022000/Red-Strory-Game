using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakrock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerTarget;
    public GameObject target;
    public Animator animator;
    void Start()
    {
        //animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTarget.transform.position.x > target.transform.position.x)
        {
            animator.enabled = true ;
        }
    }
}
