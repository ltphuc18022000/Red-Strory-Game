using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingByContact : MonoBehaviour
{
    private GameObject player;
    FaderScript faderScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //.. ending game
            Time.timeScale = 1;
            faderScript = GetComponent<FaderScript>();
            player.GetComponent<PlayerMovement>().StopMovement();
            player.GetComponent<PlayerMovement>().enabled = false;
            // credit scene - 14
            StartCoroutine(ChangeScene(14));
        }

    }
    IEnumerator ChangeScene(int s)
    {
        yield return new WaitForSeconds(faderScript.BeginFade(2));
        // start menu to change to the next scene after the fade is finished.
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }
}
