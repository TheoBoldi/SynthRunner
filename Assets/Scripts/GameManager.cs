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
    public bool gamePause = true;
    public bool isIntro = false;
    public float introDuration = 4.0f;

    [HideInInspector]
    public int life = 1;
    [HideInInspector]
    public AudioSource mainSource;

    private CameraManager _camera;
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
        _camera = FindObjectOfType<CameraManager>();
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

    public void EndIntro()
    {
        isIntro = false;
        gamePause = false;
        MusicManager.Instance.ResetMusicElements();
        MusicManager.Instance._audioSource.Play();
    }

    public void LaunchIntro()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        MusicManager.Instance._audioSource.Stop();
        _camera.transform.GetComponent<Animator>().SetTrigger("Launch");
        isIntro = true;
        SoundEffectManager.instance.LaunchGame();
        yield return new WaitForSeconds(introDuration);
        EndIntro();
    }
}
