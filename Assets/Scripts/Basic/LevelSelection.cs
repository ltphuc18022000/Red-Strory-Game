using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvButtons;
    // Start is called before the first frame update
    void Start()
    {
        int levelAt = SaveSystem.playerData.level;
        // disable button level > levelAt
        for (int i = levelAt; i < lvButtons.Length; i++) 
        {
            lvButtons[i].interactable = false;
        }
    }

}
