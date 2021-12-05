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

    public Text scoreText;
    public Text lifeText;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;
    public int score = 0;
    public int life = 3;

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
        scoreText.text = "Score : " + score.ToString();
        lifeText.text = "Life : " + life.ToString();

        if (life == 0)
        {
            Time.timeScale = 0;
            mainSource.Pause();
            gameCanvas.gameObject.SetActive(false);
            gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
