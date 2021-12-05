using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FrequenciesIntensityModifier : FrequenciesModifier
{

    public float maxIntensity;
    public float minIntensity;
    private float actualIntensity;
    private Bloom bloom;

    void Awake()
    {
        _useBeat = false;
        _useAmplitude = true;
        Volume vol = GetComponent<Volume>();
        if (vol.sharedProfile.TryGet<Bloom>(out bloom))
        {
            bloom.intensity.value = minIntensity;
            actualIntensity = minIntensity;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnIntensity(float intensity)
    {

        float clampedIntensity = Mathf.Clamp(intensity, 0, 1);

        actualIntensity = Mathf.Lerp(minIntensity, maxIntensity, clampedIntensity);
        
        bloom.intensity.value = actualIntensity;
    }
}
