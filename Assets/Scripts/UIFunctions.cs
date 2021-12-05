using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UIFunctions : MonoBehaviour
{
    private bool paused = false;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        if (!paused)
        {
            paused = true;
            GameManager.Instance.mainSource.Pause();
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            GameManager.Instance.mainSource.Play();
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
