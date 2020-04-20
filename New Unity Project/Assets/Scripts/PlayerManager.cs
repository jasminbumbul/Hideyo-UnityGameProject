using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singletone
    public static PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion
    public GameObject player;
}
