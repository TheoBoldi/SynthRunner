using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TileLight : MonoBehaviour
{
    public float _fadeInSpeed = 2.0f;
    public float _fadeOutSpeed = 1.0f;

    private Material _matInstance;
    private bool _fadeOut = true;
    private bool _playerDetected;

    void Awake()
    {
        _matInstance = transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_playerDetected)
        {
            _playerDetected = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _playerDetected)
        {
            _playerDetected = false;
        }
    }

    void Update()
    {
        Color activeColor = _matInstance.GetColor("_BaseColor");
        
        float alpha = activeColor.a;

        if (_playerDetected && _fadeOut)
        {
            _fadeOut = false;
        }
        else if(!_playerDetected && !_fadeOut)
        {
            _fadeOut = true;
        }

        if (_fadeOut)
        {
            if (alpha >= 0)
                alpha -= _fadeOutSpeed * Time.deltaTime;
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (!_fadeOut)
        {
            if (alpha <= 1)
                alpha += _fadeInSpeed * Time.deltaTime;
        }

        activeColor.a = Mathf.Clamp01(alpha);
        _matInstance.SetColor("_EmissionColor",ColorManager._activeColor);
        _matInstance.SetColor("_BaseColor",activeColor);
    }
}
