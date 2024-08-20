using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject continueButton;
    void Start ()
    {
        if (!SaveSystem.FileSaveExist())
            continueButton.SetActive(false);
    }

    public void NewGame()
    {
        // chuyển màn hình chọn nhân vật
        SceneManager.LoadScene(13);
    }

    public void ContinueGame()
    {
        // load player data
        SaveSystem.playerData = SaveSystem.loadPlayerData();

        // chuyển màn hình chọn level
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
