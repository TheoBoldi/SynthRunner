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
        public int _kickBand;
        public int _clapBand;
        public float _audioProfile;
        public int _tunnelShape;
    }

    public float _timeBetweenTracks = 5.0f;

    [SerializeField]
    public List<Music> _trackList;

    public Transform _kickCubesHolder;
    public Transform _clapCubesHolder;
    private FrequenciesScalerModifier[] _kickCubes;
    private FrequenciesScalerModifier[] _clapCubes;

    public float _spawnOffset = 4.0f;

    private float m_timer = 0f;
    private int m_activeIndex = 0;
    private bool _transition = false;

    [HideInInspector]
    public AudioSource _audioSource;
    private AudioPeer _audioPeer;
    [HideInInspector]
    public PhylloTunnel _tunnel;

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
        _kickCubes = _kickCubesHolder.GetComponentsInChildren<FrequenciesScalerModifier>();
        _clapCubes = _clapCubesHolder.GetComponentsInChildren<FrequenciesScalerModifier>();
        _audioPeer = GetComponent<AudioPeer>();
        _tunnel = FindObjectOfType<PhylloTunnel>();
        ResetMusicElements();
        _audioSource.Play();
    }

    public void ResetMusicElements()
    {
        _audioSource.clip = _trackList[m_activeIndex]._audioClip;
        foreach (FrequenciesScalerModifier cube in _kickCubes)
        {
            cube._frequencyBand = _trackList[m_activeIndex]._kickBand;
            cube.timeStep = GetActiveBPM() / (60 * 2);
        }
        foreach (FrequenciesScalerModifier cube in _clapCubes)
        {
            cube._frequencyBand = _trackList[m_activeIndex]._clapBand;
            cube.timeStep = GetActiveBPM() / (60 * 2);
        }
        _audioPeer.ChangeAudioProfile(_trackList[m_activeIndex]._audioProfile);
        _tunnel.SwitchShape(_trackList[m_activeIndex]._tunnelShape);
        m_timer = 0;
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
        if (m_timer >= GetActiveDuration() + _spawnOffset)
            return (60f / GetActiveBPM()) / _trackList[m_activeIndex]._intensityCurve.Evaluate(GetActiveDuration());
        else
            return (60f / GetActiveBPM()) / _trackList[m_activeIndex]._intensityCurve.Evaluate(m_timer + 4f);
    }

    void Update()
    {
        if (GameManager.Instance.gamePause || GameManager.Instance.isIntro) return;
        if (m_timer >= GetActiveDuration() && !_transition)
        {
            GameManager.Instance.musicPause = true;
            _transition = true;
            if (m_activeIndex >= _trackList.Count - 1)
            {
                m_activeIndex = 0;
            }
            else
            {
                m_activeIndex++;
            }

            ResetMusicElements();
            _colorManager.SwitchColor(GetActiveColor());
        }

        if (_transition)
        {
            if (m_timer >= _timeBetweenTracks)
            {
                GameManager.Instance.musicPause = false;
                _audioSource.Play();
                _transition = false;
                m_timer = 0;
            }
        }


        m_timer += Time.deltaTime;
    }

}
