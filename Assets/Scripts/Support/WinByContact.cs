using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinByContact : MonoBehaviour
{
    private Animator anim;
    private GameController gameController;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //.. win game
            anim.SetTrigger("Open");
            gameController.WinGame();
            player.GetComponent<PlayerMovement>().StopMovement();
            player.GetComponent<PlayerMovement>().enabled = false;
            
            // save player data
            int currLevel = SceneManager.GetActiveScene().buildIndex - 1;
            if (currLevel >= SaveSystem.playerData.level && currLevel <= 10)
                SaveSystem.playerData.level = currLevel + 1;
            SaveSystem.playerData.gold = gameController.GetGold();
            SaveSystem.SavePlayer();
        }

    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

}