using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAbyss : MonoBehaviour
{
    // Start is called before the first frame update
    public  float dame = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("da vao 2");
        if (collision.tag == "Player")
        {
      
            collision.GetComponent<Health>().TakeDamage(dame);
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
