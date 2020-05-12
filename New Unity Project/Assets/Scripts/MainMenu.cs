using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator trasition;
    public float animationTime = 1f;
    public void PlayGame()
    {
        StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator nextLevel(int index)
    {
        trasition.SetTrigger("start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(index);

    }
    public void Quit()
    {
        Application.Quit();
    }
}
