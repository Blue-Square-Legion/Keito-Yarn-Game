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
        //if (_colorChangeEffectCouroutine == null)
        //{
        //    particleSystemObj = Instantiate(partSystObj, gameObject.transform);
        //    particleSystemObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //    _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
        //}
        //else
        //{
        //    StopCoroutine(_colorChangeEffectCouroutine);

        //}
        if(_colorChangeEffectCouroutine != null)
        {
            //Debug.Log("couroutine already in effect. will end it", gameObject);
            StopCoroutine(_colorChangeEffectCouroutine);
            CleanUp();
        }

        if (particleSystemObj != null)
        {
            //Debug.Log(particleSystemObj, particleSystemObj);
            particleSystemObj.SetActive(true);
        }
        else
        {
            particleSystemObj = Instantiate(partSystObj, gameObject.transform);
            particleSystemObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        particleSystemObj.SetActive(true);
        _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
    }

    private IEnumerator ActivateEffectForDuration(float _effectDuration)
    {
        //Debug.Log("ColorChange effect duration Started", gameObject);
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
        _colorChangeEffectCouroutine = null;
        //Debug.Log(_colorChangeEffectCouroutine, gameObject);
        //Debug.Log("Did clean up", gameObject);
    }
}
