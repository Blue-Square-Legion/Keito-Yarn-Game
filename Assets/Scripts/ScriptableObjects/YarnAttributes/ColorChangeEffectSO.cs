using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorChangeEffect", menuName = "Effects/ColorChangeEffect")]
public class ColorChangeEffectSO : YarnBallEffectSO
{
    [SerializeField] private GameObject newBallPrefab;
    [SerializeField] private float _effectDuration = 5f; // Duration for which the effect is active
    private ColorChangeEffectHandler _handler;
    private GameObject _ball;
    [SerializeField] private GameObject particleSystemPrefab;
    private GameObject particleSystemObj;
    
    public override void CreateEffect(GameObject ball, Transform target = null) 
    {
        //Debug.Log("Color Change Effect Created", ball);

        _ball = ball;

        _handler = ball.GetComponent<ColorChangeEffectHandler>();
        if (_handler == null)
        {
            _handler = ball.AddComponent<ColorChangeEffectHandler>();
            _handler.Initialize(_effectDuration, particleSystemPrefab);
        }
    }
    public override bool ShouldApplyOnCollision()
    {
        return _handler._isEffectActive;
    }
    
    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody)
    {
        if (targetRigidbody.gameObject.GetComponent<ColorChangeEffectHandler>() != null)
        {
            Debug.Log(_ball,  _ball);
            Debug.Log(ball,  ball);
            _ball.GetComponent<ColorChangeEffectHandler>().Initialize(_effectDuration, particleSystemPrefab);
            return;
        }
        if (_handler._isEffectActive && ballRigidbody != null && targetRigidbody != null)
        {
            GameObject targetObject = targetRigidbody.gameObject;

            float originalMass = targetRigidbody.mass;
            Vector3 originalScale = targetObject.transform.localScale;
            Vector3 originalVelocity = targetRigidbody.velocity;
            Vector3 originalAngularVelocity = targetRigidbody.angularVelocity;

            Vector3 targetPosition = targetObject.transform.position;
            Quaternion targetRotation = targetObject.transform.rotation;


            Destroy(targetObject);

            if (newBallPrefab != null)
            {
                GameObject newBall = Instantiate(newBallPrefab, targetPosition, targetRotation);

                Rigidbody newBallRigidbody = newBall.GetComponent<Rigidbody>();
                if (newBallRigidbody != null)
                {
                    newBallRigidbody.mass = originalMass;
                    newBallRigidbody.velocity = originalVelocity;
                    newBallRigidbody.angularVelocity = originalAngularVelocity;
                    newBall.GetComponent<YarnCollision>().CreateLaunchEffects();
                }

                newBall.transform.localScale = originalScale;

                //Debug.Log("Replaced target with new color ball: " + newBall.name);
            }
            else
            {
                Debug.LogError("New ball prefab is not assigned.");
            }
        }
        else if(_handler._isEffectActive == false)
        {
            Debug.Log("effect is not active");
        }
        else
        {
            Debug.LogError("ColorChange effect cannot be applied: either ballRigidbody or targetRigidbody is missing.");
        }
    }
}
