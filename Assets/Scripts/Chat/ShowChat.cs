using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowChat : MonoBehaviour
{
    public Dialogue dialogue;

    public void ShowChatDialog()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
