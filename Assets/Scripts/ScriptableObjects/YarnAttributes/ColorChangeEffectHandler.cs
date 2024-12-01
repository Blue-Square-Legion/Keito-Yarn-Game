using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeEffectHandler : MonoBehaviour
{
    private Coroutine _colorChangeEffectCouroutine;
    public bool _isEffectActive;
    private Material _material;
    public void Initialize(float duration, Material _glow, Material _base)
    {

        if(_colorChangeEffectCouroutine == null)
        {
            /*CleanUp();*/
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
            _material = _base;
            gameObject.GetComponent<MeshRenderer>().material = _glow;
            //Debug.Log(gameObject.GetComponent<MeshRenderer>().material, gameObject);
            //Debug.Log("being created", gameObject);
            /*StopCoroutine(_colorChangeEffectCouroutine);*/
        }
    }

    public void ActivateEffect()
    {
        StartCoroutine(ActivateEffectForDuration(5f));
    }
    
    private IEnumerator ActivateEffectForDuration(float _effectDuration)
    {
        //Debug.Log("ColorChange effect duration Started", gameObject);

        _isEffectActive = true;
        yield return new WaitForSeconds(_effectDuration);
        CleanUp();
        //Debug.Log("ColorChange effect duration ended", gameObject);
    }

    private void CleanUp()
    {
        _isEffectActive=false;
        gameObject.GetComponent<MeshRenderer>().material = _material;
    }
}
