using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class UIFunctions : MonoBehaviour
{
    private bool paused = false;
    private bool _fadeMenu;
    private float _timerFade;

    public GameObject _mainMenuCanvas;

    public void Play()
    {
        if (GameManager.Instance.isIntro) return;
        GameManager.Instance.LaunchIntro();
        _fadeMenu = true;
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

    void Update()
    {
        if (_fadeMenu)
        {
            _timerFade += Time.deltaTime;
            _mainMenuCanvas.GetComponentInChildren<Image>().color = new Color(1,1,1,1 - _timerFade);
            if (_timerFade >= 1)
            {
                _fadeMenu = false;
                _mainMenuCanvas.SetActive(false);
            }
        }
    }
}
