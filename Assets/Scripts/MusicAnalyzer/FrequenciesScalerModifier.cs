using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequenciesScalerModifier : FrequenciesModifier
{

    public Vector3 beatScale;
    public Vector3 restScale;
    private IEnumerator MoveToScale(Vector3 _target)
    {
        Vector3 _curr = transform.localScale;
        Vector3 _initial = _curr;
        float _timer = 0;

        while (_curr != _target)
        {
            _curr = Vector3.Lerp(_initial, _target, _timer / timeToBeat);
            _timer += Time.deltaTime;

            transform.localScale = _curr;

            yield return null;
        }

        _isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatScale);
    }

}
