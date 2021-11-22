using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequenciesRotateModifier : FrequenciesModifier
{

    public Vector3 beatRotation;
    public Vector3 restRotation;
    private IEnumerator MoveToRotation(Vector3 _target)
    {
        Vector3 _curr = transform.localScale;
        Vector3 _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            transform.rotation = Quaternion.Euler(_curr);

            yield return null;
        }

        _isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isBeat) return;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(restRotation), restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToRotation");
        StartCoroutine("MoveToRotation", beatRotation);
    }

}
