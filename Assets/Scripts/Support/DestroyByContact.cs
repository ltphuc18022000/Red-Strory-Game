using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyByContact : MonoBehaviour
{
    
    public int goldBonus = 20;
    private GameController gameController;
    private AudioSource audioCollect;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        audioCollect = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // quái chết thì rớt tiền
        if (other.tag == "Player")
        {
            // tăng tiền; level càng cao càng nhiều tiền, tăng 20% mỗi level
            goldBonus = (int)( goldBonus * (1 + (SceneManager.GetActiveScene().buildIndex-2)*0.2) );
            gameController.AddGold(goldBonus);
            // play audio source
            soundManager.instance.PlaySound(audioCollect.clip);
            // destroy object
            Destroy(this.gameObject);
        }
    }

}
