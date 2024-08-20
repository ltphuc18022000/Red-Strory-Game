using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float healthValue;
    private AudioSource audioCollect;
    void Start()
    {
        audioCollect = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            soundManager.instance.PlaySound(audioCollect.clip);
            collision.GetComponent<Health>().RecoverHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
