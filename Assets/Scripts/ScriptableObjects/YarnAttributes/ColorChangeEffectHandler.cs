using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeEffectHandler : MonoBehaviour
{
    private Coroutine _colorChangeEffectCouroutine;
    public bool _isEffectActive;

    public void Initialize(float duration)
    {
        if (_colorChangeEffectCouroutine == null)
        {
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
        }
    }
    private IEnumerator ActivateEffectForDuration(float _effectDuration)
    {
        Debug.Log("ColorChange effect duration Started");
        _isEffectActive = true;
        yield return new WaitForSeconds(_effectDuration);
        _isEffectActive = false;
        Debug.Log("ColorChange effect duration ended");
    }

}
