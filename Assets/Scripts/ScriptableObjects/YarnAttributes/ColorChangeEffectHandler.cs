using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeEffectHandler : MonoBehaviour
{
    private Coroutine _colorChangeEffectCouroutine;
    public bool _isEffectActive;
    private Material _material;
    private float oldScale = 0f;
    private MeshRenderer mr;
    [SerializeField] private Material _glowMaterial;

    private void Awake()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    // Starts the effect if it is not already started
    public void Initialize(float duration, Material _glow, Material _base)
    {
        // if (_colorChangeEffectCouroutine == null || oldScale < transform.localScale.x)

        // Enable effect when initialized
        if (_colorChangeEffectCouroutine == null)
        {
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
            _material = _base;
            mr.material = _glow;
            oldScale = transform.localScale.x;
        }
        // If ball grew, re-apply effect
        else if (oldScale < transform.localScale.x) {
            StopCoroutine(_colorChangeEffectCouroutine); // Stop the coroutine if it's still running so it can be reset properly
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
            mr.material = _glow;
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
        // _colorChangeEffectCouroutine = null;
    }

    // For some reason, glow material and isEffectActive don't match up.
    // This is to make sure that the effect is only applied when 
    public bool isGlowing() {
        return mr.material.name.Contains("Glow");
    }
}
