using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [System.Serializable]
    public struct Music
    {
        public AudioClip _audioClip;
        public float _BPM;
        public Color _color;
        public AnimationCurve _intensityCurve;
        public float _clipDuration;
    }

    public float _timeBetweenTracks = 5.0f;

    [SerializeField]
    public List<Music> _trackList;

    private float m_timer = 0f;
    private int m_activeIndex = 0;
    private bool _transition = false;

    private AudioSource _audioSource;

    [HideInInspector]
    public ColorManager _colorManager;


    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
        _colorManager = GetComponent<ColorManager>();
        _audioSource.clip = _trackList[m_activeIndex]._audioClip;
        _audioSource.Play();
    }


    public float GetActiveIntensity()
    {
        return _trackList[m_activeIndex]._intensityCurve.Evaluate(m_timer);
    }
    public float GetActiveDuration()
    {
        return _trackList[m_activeIndex]._clipDuration;
    }
    public float GetActiveBPM()
    {
        return _trackList[m_activeIndex]._BPM;
    }

    public Color GetActiveColor()
    {
        return _trackList[m_activeIndex]._color;
    }
    public float GetActiveFrequency()
    {
        return (60f / GetActiveBPM()) / GetActiveIntensity();
    }

    void Update()
    {
        if (m_timer >= GetActiveDuration() && !_transition)
        {
            _transition = true;
            m_timer = 0;
            m_activeIndex++;
            _audioSource.clip = _trackList[m_activeIndex]._audioClip;
            _colorManager.SwitchColor(GetActiveColor());
        }

        if (_transition)
        {
            if (m_timer >= _timeBetweenTracks)
            {
                _audioSource.Play();
                _transition = false;
            }
        }


        m_timer += Time.deltaTime;
    }
}
