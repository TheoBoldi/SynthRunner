using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Phyllotaxis : MonoBehaviour
{
    
    public float _degree, _scale;
    public int _numberStart;
    public int _stepSize;
    public int _maxIteration;
    
    public bool _useLerping;
    public float _intervalLerp;
    private bool _isLerping;
    private Vector3 _startPos, _endPos;
    private float _timeStartedLerp;

    private int _number;
    private int _currentIteration;

    public bool _spawnObject;
    public GameObject _objectToSpawn;
    public Vector3 _objectScale;
    private Vector2 m_currentPos;

    private TrailRenderer _trailRenderer;


    void StartLerping()
    {
        _isLerping = true;
        _timeStartedLerp = Time.time;
        m_currentPos = CalculatePhyllo(_degree, _scale, _number);
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
        if (_spawnObject) return;

        _trailRenderer = GetComponent<TrailRenderer>();
        _number = _numberStart;
        transform.localPosition = CalculatePhyllo(_degree, _scale, _number);
        if(_useLerping)
            StartLerping();
    }

    void FixedUpdate()
    {
        if (_spawnObject) return;
        
        if (_useLerping)
        {
            if (_isLerping)
            {
                float timeSinceStarted = Time.time - _timeStartedLerp;
                float percentageComplete = timeSinceStarted / _intervalLerp;
                transform.localPosition = Vector3.Lerp(_startPos, _endPos, percentageComplete);

                if (percentageComplete >= 0.97f)
                {
                    transform.localPosition = _endPos;
                    _number += _stepSize;
                    _currentIteration++;
                    if (_currentIteration <= _maxIteration)
                    {
                        StartLerping();
                    }
                    else
                    {
                        _isLerping = false;
                    }
                }
            }
        }
        else
        {
            m_currentPos = CalculatePhyllo(_degree, _scale, _number);
            transform.localPosition = m_currentPos;
            _number+= _stepSize;
            _currentIteration++;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!_spawnObject) return;
        if (Input.GetKey(KeyCode.Space))
        {
            m_currentPos = CalculatePhyllo(_degree, _scale, _number);
            GameObject objectInstance = Instantiate(_objectToSpawn);
            objectInstance.transform.position = m_currentPos;
            objectInstance.transform.localScale = _objectScale;
            _number++;
        }
    }
}
