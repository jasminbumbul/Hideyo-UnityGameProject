using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHelp : MonoBehaviour
{
    public GameObject Help;
    public GameObject Pause;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void activateHelp()
    {
        Pause.SetActive(false);
        Help.SetActive(true);
    }

    
    public void activatePause()
    {
        Help.SetActive(false);
        Pause.SetActive(true);
    }
}
