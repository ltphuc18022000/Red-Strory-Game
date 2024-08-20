using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterSenconds : MonoBehaviour
{
    FaderScript faderScript;
    void Start()
    {
        // wait 10s for credit show all information
        faderScript = GetComponent<FaderScript>();
        StartCoroutine(Wait(7f));
    }

    IEnumerator Wait(float s)
    {
        yield return new WaitForSeconds(s);
        StartCoroutine(ChangeScene(0));
    }
    IEnumerator ChangeScene(int s)
    {
        yield return new WaitForSeconds(faderScript.BeginFade(2));
        SceneManager.LoadScene(s, LoadSceneMode.Single);
    }
}
