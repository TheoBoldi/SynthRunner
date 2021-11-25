using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FrequenciesIntensityModifier : FrequenciesModifier
{

    public float maxIntensity;
    public float minIntensity;
    private float actualIntensity;
    private PostProcessVolume _volume;
    private Bloom bloom;

    void Awake()
    {
        _useBeat = false;
        _useAmplitude = true;
        _volume = GetComponent<PostProcessVolume>();
        _volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = minIntensity;
        actualIntensity = minIntensity;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnIntensity(float intensity)
    {

        float clampedIntensity = Mathf.Clamp(intensity, 0, 1);

        actualIntensity = Mathf.Lerp(minIntensity, maxIntensity, clampedIntensity);
        
        _volume = gameObject.GetComponent<PostProcessVolume>();
        Bloom bloom;
        _volume.profile.TryGetSettings(out bloom);
        bloom.intensity.value = actualIntensity;
    }
}
