using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text goldText;
    public Text endText; // Win/lose
    public GameObject nextLevelButton;
    public GameObject settingButton;
    public GameObject restartButton;
    public GameObject resumeButton;
    public GameObject exitButton; // back to menu

    private int gold;
    private PlayerData playerData;

    public Text skillsNumberText;

    private GameObject winObjects;
    public bool needKillBoss = false;

    FaderScript FaderScript;

    void Start()
    {
        // get gold from SaveSystem
        PlayerData playerData = SaveSystem.loadPlayerData();
        gold = SaveSystem.playerData.gold;

        restartButton.SetActive(false);
        nextLevelButton.SetActive(false);
        settingButton.SetActive(false);
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        SetEndText("");
        SetGoldText();

        if (needKillBoss)
        {
            winObjects = GameObject.FindGameObjectWithTag("WinObjects");
            for (int a = 0; a < winObjects.transform.childCount; a++)
            {
                winObjects.transform.GetChild(a).gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    // Number of skill can use
    public void SetSkillsNumberText(int s)
    {
        skillsNumberText.text = "x" + s.ToString();
    }

    // gold
    public void AddGold(int s)
    {
        gold += s;
        SetGoldText();
    }

    public void SubtractGold(int s)
    {
        gold -= s;
        SetGoldText();
    }

    private void SetGoldText()
    {
        // format gold 1000000 -> 1.000.000
        var text = gold.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("de"));
        goldText.text = text;
    }

    public int GetGold()
    {
        return gold;
    }

    // end text (win/lose)
    public void SetEndText(string text)
    {
        endText.text = text;
    }

    // win game
    public void WinGame()
    {
        restartButton.SetActive(true);
        nextLevelButton.SetActive(true);
        settingButton.SetActive(true);
        exitButton.SetActive(true);
        SetEndText("Win");
    }
    // lose game
    public void LoseGame()
    {
        restartButton.SetActive(true);
        settingButton.SetActive(true);
        exitButton.SetActive(true);
        SetEndText("Lose");
    }

    // function of button
    public void NextLevel()
    {
        Time.timeScale = 1;
        FaderScript = GetComponent<FaderScript>();
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(ChangeLevel(nextScene));
    }

    public void Restart()
    {
        Time.timeScale = 1;
        FaderScript = GetComponent<FaderScript>();
        int level = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ChangeLevel(level));
    }

    public void Pause()
    {
        restartButton.SetActive(true);
        settingButton.SetActive(true);
        resumeButton.SetActive(true);
        exitButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        restartButton.SetActive(false);
        settingButton.SetActive(false);
        resumeButton.SetActive(false);
        exitButton.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        // back to menu
        Time.timeScale = 1;
        FaderScript = GetComponent<FaderScript>();
        StartCoroutine(ChangeLevel(0));
    }

    IEnumerator ChangeLevel(int level)
    {
        yield return new WaitForSeconds(FaderScript.BeginFade(2));
        // start menu to change to the next scene after the fade is finished.
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
}
