using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject _sampleGameObject;
    private GameObject[] _sampleCubes = new GameObject[512];
    public float _maxScale;

    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            GameObject _instantiateSampleCube = (GameObject) Instantiate(_sampleGameObject);
            _instantiateSampleCube.transform.position = this.transform.position;
            _instantiateSampleCube.transform.parent = this.transform;
            _instantiateSampleCube.name = "SampleCube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instantiateSampleCube.transform.position = Vector3.forward * 100;
            _sampleCubes[i] = _instantiateSampleCube;
        }
    }

    void Update()
    {
        if (_sampleCubes != null)
        {
            for (int i = 0; i < 512; i++)
            {
                if(_sampleCubes[i] != null)
                {
                    _sampleCubes[i].transform.localScale = new Vector3(1, (AudioPeer._samples[i] * _maxScale) + 2, 1);

                }
            }
        }
            
    }
}
