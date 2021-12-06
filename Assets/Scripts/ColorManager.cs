using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public float _minIntensity;
    public float _maxIntensity;

    public float _timeSwitchColor;
    private float _timerSwitchColor;

    public List<Material> _glowyMaterials;
    public List<Material> _colorMaterials;
    private List<Material> _tilesMaterials;

    private Phyllotaxis[] _phyllos;

    private Color _baseColor;
    public static Color _activeColor;
    private Color _targetColor;
    
    private bool _lerpingColor = false;

    void Awake()
    {
        _phyllos = FindObjectsOfType<Phyllotaxis>();
    }
    void Start()
    {
        _baseColor = MusicManager.Instance.GetActiveColor();
        foreach (Phyllotaxis phyllo in _phyllos)
        {
            phyllo.SetTrailColor(_baseColor);
        }
        foreach (Material mat in _colorMaterials)
        {
            mat.SetColor("_BaseColor", _baseColor);
            if (mat.name != "Chara" && mat.name != "Border")
                mat.SetColor("_EmissionColor", _baseColor);
        }
        foreach (Material mat in _glowyMaterials)
        {
            mat.SetColor("_EmissionColor", _baseColor * _minIntensity);
            
        }

        _activeColor = _baseColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (_lerpingColor)
        {
            _timerSwitchColor += Time.deltaTime;
            _activeColor = Color.Lerp(_baseColor, _targetColor, Mathf.Clamp01(_timerSwitchColor / _timeSwitchColor));
            foreach (Phyllotaxis phyllo in _phyllos)
            {
                phyllo.SetTrailColor(_activeColor);
            }
            foreach (Material mat in _colorMaterials)
            {
                mat.SetColor("_BaseColor", _activeColor);
                if(mat.name != "Chara" && mat.name != "Border")
                mat.SetColor("_EmissionColor", _activeColor);
                if (_timerSwitchColor >= _timeSwitchColor)
                {
                    mat.SetColor("_BaseColor", _targetColor);
                    _lerpingColor = false;
                    _baseColor = _targetColor;
                    _activeColor = _baseColor;
                }
            }
            
        }
        else
        {
            UpdateIntensity();
        }
    }

    void UpdateIntensity()
    {
        foreach (Material mat in _glowyMaterials)
        {
            mat.SetColor("_EmissionColor", _baseColor * Mathf.Lerp(_minIntensity,_maxIntensity,AudioPeer._amplitudeBuffer));
        }
    }

    public void SwitchColor(Color _target)
    {
        _timerSwitchColor = 0f;
        _targetColor = _target;
        _lerpingColor = true;
        foreach (Material mat in _glowyMaterials)
        {
            mat.SetColor("_EmissionColor", _baseColor * _minIntensity);
        }
    }
}
