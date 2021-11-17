using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public int score = 0;
    public int life = 3;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(life == 0)
        {
            Time.timeScale = 0;
            gameCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
