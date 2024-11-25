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
        if (particleSystemObj == null)
        {
            particleSystemObj = Instantiate(partSystObj, gameObject.transform);
        }

        if(_colorChangeEffectCouroutine == null)
        {
            /*CleanUp();*/
            _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
            Debug.Log("being created", gameObject);
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
        particleSystemObj.SetActive(true);
        _isEffectActive = true;
        yield return new WaitForSeconds(_effectDuration);
        CleanUp();
        //Debug.Log("ColorChange effect duration ended", gameObject);
        //Destroy(particleSystemObj);
    }

    private void CleanUp()
    {
        _isEffectActive=false;
        particleSystemObj?.SetActive(false);
    }
}
