using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOptions : MonoBehaviour
{
    public GameObject Options;
    public GameObject Pause;
    private Camera camera;
    void Start()
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {

    }

    public void activateOptions()
    {
        Pause.SetActive(false);
        Options.SetActive(true);
    }


    public void activatePause()
    {
        Options.SetActive(false);
        Pause.SetActive(true);
    }

    public void lowClippingPlanes()
    {
        camera.farClipPlane = 100;
    }

    public void highClippingPlanes()
    {
        camera.farClipPlane = 400;
    }
}
