using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCivilInteract : MonoBehaviour
{
    private GameObject[] Humans;
    public GameObject InteractText;
    public DialogueTrigger dialogueTrigger;
    public bool triggered = false;
    public static PlayerCivilInteract instance;
    public float distance = 0f;
    public float minDistance = 1000f;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Humans=GameObject.FindGameObjectsWithTag("HumanInteract");
    }

    void Update()
    {

        minDistance = 1000f;
        foreach (var human in Humans)
        {
            distance = Vector3.Distance(transform.position, human.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                dialogueTrigger = human.GetComponent<DialogueTrigger>();
            }
        }


        if (minDistance < 4)
        {
            InteractText.SetActive(true);
            if (triggered == false && Input.GetKey(KeyCode.E))
            {
                Cursor.lockState = CursorLockMode.None;
                dialogueTrigger.TriggerDialogue();
                triggered = true;
            }
        }
        if ((minDistance>4 && triggered))
        {
            Cursor.lockState = CursorLockMode.Locked;
            InteractText.SetActive(false);
            dialogueTrigger.StopDialogue();
            triggered = false;
        }
        if(minDistance>4 && !triggered)
        {
            InteractText.SetActive(false);
        }
    }
}
