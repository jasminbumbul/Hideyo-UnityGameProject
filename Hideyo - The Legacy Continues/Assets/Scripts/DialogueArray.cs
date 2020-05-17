using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArray : MonoBehaviour
{
    public DialogueTrigger[] DialogueTriggers;
    public static DialogueArray instance;

    public void Awake()
    {
        instance = this;
    }
}
