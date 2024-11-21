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
        if(_colorChangeEffectCouroutine != null)
        {
            StopCoroutine(_colorChangeEffectCouroutine);
            CleanUp();
        }

        if (particleSystemObj == null)
        {
            particleSystemObj = Instantiate(partSystObj, gameObject.transform);
            //particleSystemObj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            //particleSystemObj.transform.localScale = gameObject.transform.localScale;
        }
        Debug.Log("being created", gameObject);
        _colorChangeEffectCouroutine = StartCoroutine(ActivateEffectForDuration(duration));
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
        _colorChangeEffectCouroutine = null;
        //Debug.Log("clean up");
        //Debug.Log("Did clean up", gameObject);
    }
}
