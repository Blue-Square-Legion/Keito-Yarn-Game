using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeEffectHandler : MonoBehaviour
{
    private Coroutine _colorChangeEffectCouroutine;
    public bool _isEffectActive;
    private GameObject particleSystemObj;
    public void Initialize(float duration, GameObject partSystObj)
    {
        if (_colorChangeEffectCouroutine == null)
        {
            //particleSystemObj = Instantiate(partSystObj, gameObject.transform);
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
        Destroy(particleSystemObj);
    }

}
