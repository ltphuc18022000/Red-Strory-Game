using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float dame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {   
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>()!= null)
            {
                collision.GetComponent<Health>().TakeDamage(dame);
            }
        
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
