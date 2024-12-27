using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeEffectHandler : MonoBehaviour
{
    private Coroutine _colorChangeEffectCouroutine;
    public bool _isEffectActive;
    private Material _material;
    private float oldScale = 0f;

    // Starts the effect if it is not already started
    public void Initialize(float duration, Material _glow, Material _base)
    {
        // If ball grew, re-apply effect
        if(_colorChangeEffectCouroutine == null && oldScale < transform.localScale.x)
        {
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
            _material = _base;
            gameObject.GetComponent<MeshRenderer>().material = _glow;
            oldScale = transform.localScale.x;
        }
    }

    public void ActivateEffect()
    {
        StartCoroutine(ActivateEffectForDuration(5f));
    }
    
    private IEnumerator ActivateEffectForDuration(float _effectDuration)
    {
        _isEffectActive = true;
        yield return new WaitForSeconds(_effectDuration);
        CleanUp();
    }

    private void CleanUp()
    {
        _isEffectActive=false;
        gameObject.GetComponent<MeshRenderer>().material = _material;
        _colorChangeEffectCouroutine = null;
    }
}
