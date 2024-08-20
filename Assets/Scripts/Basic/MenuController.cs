using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private FaderScript FaderScript;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        FaderScript = GetComponent<FaderScript>();
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(FaderScript.BeginFade(2));
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
