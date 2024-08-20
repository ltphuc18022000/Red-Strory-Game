using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth { get; set; }
    private Animator animation;
    private GameController gameController;

    // Start is called before the first frame update
    private bool dead = false;
    public AudioClip hurt;
    public AudioClip dieAudio;
    void Start()
    {
        // get health from player data
        PlayerData playerData = SaveSystem.playerData;
        startingHealth = playerData.health;
        currentHealth = playerData.health;

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        animation = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0 , startingHealth);
        if (currentHealth > 0)
        {
            soundManager.instance.PlaySound(hurt);
            animation.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                soundManager.instance.PlaySound(dieAudio);
                animation.SetTrigger("dead");
                // hết máu sẽ endgame
                //FindObjectOfType<EndGame>().GameOver();
                gameController.LoseGame();
                //StartCoroutine(Wait(1.5f));
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
           
        }
    }

    // hồi máu nếu lượm được cục máu
    public void RecoverHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
    }

}
