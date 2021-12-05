using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    private AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _freqBands = new float[8]; 
    public static float[] _bandBuffer = new float[8];

    private float[] _bufferDecrease = new float[8];
    
    private float[] _freqBandsHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float _amplitude;
    private float _amplitudeHighest;
    public static float _amplitudeBuffer;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumData();
        MakeFrequenciesBands();
        MakeBandBuffer();
        MakeClampedAudioBands();
        GetAmplitude();
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += _audioBand[i];
            currentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (currentAmplitude > _amplitudeHighest)
            _amplitudeHighest = currentAmplitude;

        _amplitude = currentAmplitude / _amplitudeHighest;
        _amplitudeBuffer = currentAmplitudeBuffer / _amplitudeHighest;
    }
    void GetSpectrumData()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeBandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBands[i];
                _bufferDecrease[i] = 0.005f;
            }

            if (_freqBands[i] < _bandBuffer[i])
            {
                _bandBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;

            }
        }
    }

    void MakeFrequenciesBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            
            float average = 0f;
            int sampleCount = (int) Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBands[i] = average * 8;

        }
    }

    void MakeClampedAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBands[i] > _freqBandsHighest[i])
            {
                _freqBandsHighest[i] = _freqBands[i];
            }

            _audioBand[i] = (_freqBands[i] / _freqBandsHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandsHighest[i]);
        }
    }

    public void ChangeAudioProfile(float profile)
    {
        for (int i = 0; i < _freqBandsHighest.Length; i++)
        {
            _freqBandsHighest[i] = profile;
        }

        _amplitudeHighest = profile;
    }
}
