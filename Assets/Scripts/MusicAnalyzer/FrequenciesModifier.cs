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

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;

    protected bool m_isBeat;

    public int _frequencyBand;
    public bool _useBuffer = false;

    public virtual void OnBeat()
    {
        m_timer = 0;
        m_isBeat = true;
    }

    public virtual void OnUpdate()
    {
        m_previousAudioValue = m_audioValue;
        m_audioValue = _useBuffer ? AudioPeer._bandBuffer[_frequencyBand] : AudioPeer._freqBands[_frequencyBand];
        Debug.Log("Band Previous Value : " + m_previousAudioValue);
        Debug.Log("Band Actual Value : " + m_audioValue);

        if (m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            if (m_timer > timeStep)
                OnBeat();
        }

        if (m_previousAudioValue <= bias &&
            m_audioValue > bias)
        {
            if (m_timer > timeStep)
                OnBeat();
        }

        m_timer += Time.deltaTime;
    }

    private void Update()
    {
        OnUpdate();
    }
   
}
