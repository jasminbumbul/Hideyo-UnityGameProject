using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingBetweenScenes : MonoBehaviour
{
    static SavingBetweenScenes instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // In first scene, make us the singleton.
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
            Destroy(this); // On reload, singleton already set, so destroy duplicate.
       
    }
}
