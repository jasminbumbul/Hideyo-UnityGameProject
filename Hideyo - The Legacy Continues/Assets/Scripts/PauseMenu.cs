using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator transition;
    public float animationTime = 1f;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Resume();
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex - 1));

    }
    public  void LoadScene()
    {
        Time.timeScale = 1f;
       

        StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex+1));

    }
    public void Quit()
    {
        Application.Quit();
    }
   public  IEnumerator nextLevel(int index)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(index);

    }
}
