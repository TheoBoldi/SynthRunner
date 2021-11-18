using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrequenciesModifier : MonoBehaviour
{
    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    private float _previousAudioValue;
    private float _audioValue;
    private float _timer;

    protected bool _isBeat;

    public int _frequencyBand;
    public bool _useBuffer = false;

    public virtual void OnBeat()
    {
        _timer = 0;
        _isBeat = true;
    }

    public virtual void OnUpdate()
    {
        _previousAudioValue = _audioValue;
        _audioValue = _useBuffer ? AudioPeer._bandBuffer[_frequencyBand] : AudioPeer._freqBands[_frequencyBand];
        if (_previousAudioValue > bias &&
            _audioValue <= bias)
        {
            if (_timer > timeStep)
                OnBeat();
        }

        if (_previousAudioValue <= bias &&
            _audioValue > bias)
        {
            if (_timer > timeStep)
                OnBeat();
        }

        _timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }
    //public int _frequencyBand;
    //public bool _useBuffer = false;
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    OnUpdate();
    //}

    //public virtual void OnUpdate()
    //{

    //}
}
