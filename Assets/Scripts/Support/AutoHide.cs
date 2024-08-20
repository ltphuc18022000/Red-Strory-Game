using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    public float dideAfterSeconds = 4.0f;

    void Start()
    {
        StartCoroutine(Hide(dideAfterSeconds));
    }

    IEnumerator Hide(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
