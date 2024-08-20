using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public Text OnText;
    public Text OffText;
    private static bool muted = false;
    public static bool isPauseGame = false;
    private GameObject[] pause;
    private void Start()
    {
        pauseMenu.SetActive(false);
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            load();
        }
        else { load(); }
        UpdateTextOnOff();
        AudioListener.pause = muted;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPauseGame = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPauseGame = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
        //Destroy(HealthBar); //neeus muốn destroy 1 gameobject có thiêt slaapj dontdestroyonload khi quay lại màn hình main menu
    }
    public void OnPressBtn()
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
            OnText.enabled = false;
            OffText.enabled = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
            OnText.enabled = true;
            OffText.enabled = false;
        }
        save();
    }
    private void save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
    private void load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void UpdateTextOnOff()
    {
        if(muted == true)
        {
            OnText.enabled = false;
            OffText.enabled = true;
        }
        else
        {
            OnText.enabled = true;
            OffText.enabled = false;

        }
    }

}
