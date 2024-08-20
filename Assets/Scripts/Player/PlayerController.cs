using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public RuntimeAnimatorController anim2;

    private GameController gameController;
    private PlayerMovement playerMovement;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        // set player 2
        if (SaveSystem.playerData.playerID == 2)
            this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
    }


    // collect fire ball
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnableFire")
        {
            playerMovement.timesFire = 10;
            gameController.SetSkillsNumberText(10);
        }
    }

    // hidden ground - using UnityEngine.Tilemaps;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "HiddenGround")
        {
            // disable hiddenmap
            other.gameObject.GetComponent<TilemapRenderer>().enabled = false;
            other.gameObject.GetComponent<TilemapCollider2D>().enabled = false;
        }
    }
}
