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
        //StartCoroutine(nextLevel(3));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("IntroCutScene");
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
