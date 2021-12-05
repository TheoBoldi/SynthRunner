using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhylloTunnel : MonoBehaviour
{
    public Transform _tunnel;
    public float _tunnelSpeed;
    public float _cameraDistance;
    public Phyllotaxis _phyllo;
    private int activeShape = 0;
    private float activeAngle = 0;
    private float activeForce = 0;

    [System.Serializable]
    public struct Shapes
    {
        public float softAngle;
        public float turnAngle;
        public float hardTurnAngle;
    }

    [SerializeField]
    public List<Shapes> _shapes;

    public void SwitchShape(int index)
    {
        activeShape = index;
        activeForce = 0;
        _phyllo._degree = _shapes[index].softAngle;
    }

    public void RotateShape(bool increase)
    {
        if (increase)
        {
            switch (activeForce)
            {
                case 0:
                    activeForce++;
                    _phyllo._degree = _shapes[activeShape].turnAngle;
                    break;
                case 1:
                    activeForce++;
                    _phyllo._degree = _shapes[activeShape].hardTurnAngle;
                    break;
                case 2:
                    break;
            }
        }
        else
        {
            switch (activeForce)
            {
                case 0:
                    break;
                case 1:
                    activeForce--;
                    _phyllo._degree = _shapes[activeShape].softAngle;
                    break;
                case 2:
                    activeForce--;
                    _phyllo._degree = _shapes[activeShape].turnAngle;
                    break;
            }
        }
    }

    void Update()
    {
        if (float.IsNaN(AudioPeer._amplitude)) return;

        _tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y,
            _tunnel.position.z + (AudioPeer._amplitude * _tunnelSpeed));


        //if(_tunnel.position.z < -30)
        //_tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y, _cameraDistance);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,
            _tunnel.position.z - _cameraDistance);
    }


}
