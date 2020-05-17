using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void TriggerDialogue(bool hasCoins)
    {
        if (hasCoins)
        {
         
            FindObjectOfType<DialogueManager>().StartDialogue(DialogueArray.instance.DialogueTriggers[1].dialogue);

        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(DialogueArray.instance.DialogueTriggers[0].dialogue);

        }
    }

}
