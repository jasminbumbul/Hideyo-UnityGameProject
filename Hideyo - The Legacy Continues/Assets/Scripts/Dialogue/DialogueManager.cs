using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    private Animator animator;

    private void Start()
    {
        sentences=new Queue<string>();
        animator=GameObject.Find("DialogueBox").GetComponent<Animator>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen",true);
        nameText.text=dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count==0)
        {
            EndDialogue();
            return;
        }
        
        string sentence=sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text="";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text+=letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen",false);
    }

    public void EndConversation()
    {
        sentences.Clear();
        animator.SetBool("IsOpen", false);

    }
}
