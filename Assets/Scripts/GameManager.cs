using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public bool musicPause = false;
    public bool gamePause = false;
    public bool playerPause = false;
    
    [HideInInspector]
    public int life = 1;
    [HideInInspector]
    public AudioSource mainSource;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
        mainSource = GameObject.FindObjectOfType<MusicManager>().transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0)
        {
            Time.timeScale = 0;
            mainSource.Pause();
            gameCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
