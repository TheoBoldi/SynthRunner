using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Phyllotaxis : MonoBehaviour
{
    private Material _trailMat;
    public Color _trailColor;

    public float _degree, _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;
    
    public bool _useLerping;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _lerpPosTimer, _lerpPosSpeed;
    public Vector2 _lerpPosSpeedMinMax;
    public AnimationCurve _lerpPosAnimCurve;
    public int _lerpPosBand;

    private int _number;
    private int _currentIteration;

    private Vector2 m_currentPos;

    private TrailRenderer _trailRenderer;

    //Repeat
    private bool _forward;
    public bool _repeat, _invert;

    //Scaling
    public bool _useScaleAnimation, _useScaleCurve;
    public Vector2 _scaleAnimMinMax;
    public AnimationCurve _scaleAnimCurve;
    public float _scaleAnimSpeed;
    public int _scaleBand;
    private float _scaleTimer, _currentScale;

    void SetLerpPosition()
    {
        m_currentPos = CalculatePhyllo(_degree, _currentScale, _number);
        _startPos = this.transform.localPosition;
        _endPos = m_currentPos;
    }

    private Vector2 CalculatePhyllo(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);

        float r = scale * Mathf.Sqrt(count);

        float x = r * (float) System.Math.Cos(angle);
        float y = r * (float) System.Math.Sin(angle);

        return new Vector2(x, y);
    }


    void Awake()
    {
        _currentScale = _scale;
        _forward = true;

        _trailRenderer = GetComponent<TrailRenderer>();
        _trailMat = new Material(_trailRenderer.material);
        _trailMat.SetColor("_TintColor", _trailColor);
        _trailRenderer.material = _trailMat;

        _number = _numberStart;
        transform.localPosition = CalculatePhyllo(_degree, _currentScale, _number);
        if (_useLerping)
        {
            _isLerping = true;
            SetLerpPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_useScaleAnimation)
        {
            if (_useScaleCurve)
            {
                if (float.IsNaN(AudioPeer._audioBand[_lerpPosBand])) return;

                _scaleTimer += Time.deltaTime * _scaleAnimSpeed * AudioPeer._audioBand[_scaleBand];
                if (_scaleTimer >= 1)
                {
                    _scaleTimer -= 1;
                }

                _currentScale = Mathf.Lerp(_scaleAnimMinMax.x, _scaleAnimMinMax.y,
                    _scaleAnimCurve.Evaluate(_scaleTimer));
            }
            else
            {
                _currentScale = Mathf.Lerp(_scaleAnimMinMax.x, _scaleAnimMinMax.y, AudioPeer._audioBand[_scaleBand]);
            }
        }

        if (_useLerping)
        {
            if (_isLerping)
            {
                if (float.IsNaN(AudioPeer._audioBand[_lerpPosBand])) return;

                float audioValue = AudioPeer._audioBand[_lerpPosBand];
                _lerpPosSpeed = Mathf.Lerp(_lerpPosSpeedMinMax.x, _lerpPosSpeedMinMax.y,
                    _lerpPosAnimCurve.Evaluate(audioValue));
                
                _lerpPosTimer += Time.deltaTime * _lerpPosSpeed;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, Mathf.Clamp01(_lerpPosTimer));
                if (_lerpPosTimer >= 1)
                {
                    _lerpPosTimer -= 1;

                    if (_forward)
                    {
                        _number += _stepSize;
                        _currentIteration++;

                    }
                    else
                    {
                        _number -= _stepSize;
                        _currentIteration--;
                    }

                    if (_currentIteration > 0 && _currentIteration < _maxIteration)
                    {
                        SetLerpPosition();
                    }
                    else
                    {
                        if (_repeat)
                        {
                            if (_invert)
                            {
                                _forward = !_forward;
                                SetLerpPosition();
                            }
                            else
                            {
                                _number = _numberStart;
                                _currentIteration = 0;
                                SetLerpPosition();
                            }
                        }
                        else
                        {
                            _isLerping = false;
                        }
                    }
                }
            }
        }
        if(!_useLerping)
        {
            m_currentPos = CalculatePhyllo(_degree, _currentScale, _number);
            transform.localPosition = m_currentPos;
            _number += _stepSize;
            _currentIteration++;
        }
    }
}
