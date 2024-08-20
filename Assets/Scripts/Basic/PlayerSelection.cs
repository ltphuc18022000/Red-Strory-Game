using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection : MonoBehaviour
{
    public GameObject InfoPlayer1;
    public GameObject InfoPlayer2;

    void Start()
    {
        // Create Player default information
        // PlayerData(int playerID, int gold, int level, float health, int attackDamage, float speedRun, float jumbHeight)
        // data test -> JUST CLICK CONFIRM
        // PlayerData playerData = new PlayerData(1, 0, 11, 10f, 100, 15f, 23.0f);
        PlayerData playerData = new PlayerData(1, 0, 1, 2.0f, 30, 9.0f, 23.0f);
        SaveSystem.playerData = playerData;

        InfoPlayer1.SetActive(true);
        InfoPlayer2.SetActive(false);
    }

    public void SelectPlayer1()
    {
        InfoPlayer1.SetActive(true);
        InfoPlayer2.SetActive(false);

        // edit playerData
        SaveSystem.playerData.playerID = 1;
        SaveSystem.playerData.attackDamage = 30;
        SaveSystem.playerData.speedRun = 9.0f;
    }
    public void SelectPlayer2()
    {
        InfoPlayer1.SetActive(false);
        InfoPlayer2.SetActive(true);

        // edit playerData
        SaveSystem.playerData.playerID = 2;
        SaveSystem.playerData.attackDamage = 20;
        SaveSystem.playerData.speedRun = 12.0f;
    }

    public void ConfirmPlayer()
    {
        SaveSystem.SavePlayer();
        SceneManager.LoadScene(1);
    }
}
