using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTunnel : MonoBehaviour
{
    public Transform _tunnel;
    public float _tunnelSpeed;
    public float _cameraDistance;
    
    void Update()
    {
        if (float.IsNaN(AudioPeer._amplitude)) return;

        _tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y,
            _tunnel.position.z - (AudioPeer._amplitude * _tunnelSpeed));


        //if(_tunnel.position.z < -30)
        //_tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y, _cameraDistance);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,
            _tunnel.position.z - _cameraDistance);
    }
}
