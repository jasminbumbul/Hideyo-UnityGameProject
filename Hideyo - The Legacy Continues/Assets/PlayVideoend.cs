using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PlayVideoend : MonoBehaviour
{
    public Animator transition;
    private float animationTime=1f;
    private float timer = 0.0f;
    private void Update()
    {
        timer+=Time.deltaTime;
        timer%=60;
        if(timer>45)
        {
           StartCoroutine(nextLevel(0));
        }
    }

   IEnumerator nextLevel(int index)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(index);
    }
}
