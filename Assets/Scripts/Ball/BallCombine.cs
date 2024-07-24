using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallCombine : MonoBehaviour
{
    [SerializeField] private YarnAttributesSO yarnAttributesSO;

    [Space(5)]
    public UnityEvent OnCombine;
    public UnityEvent OnMaxSize;

    private Rigidbody _rigidBody;
    private Renderer _renderer;
    private Vector3 _scaleVectorCap;
    private ColorController _colorController;

    public string YarnCombineSound = "Play_Yarn_Combine";

    public Color Color => _renderer.material.color;
    public bool IsDamaged => _colorController.isDamaged(); 

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();

        _scaleVectorCap = new Vector3(yarnAttributesSO.scaleCap, yarnAttributesSO.scaleCap, yarnAttributesSO.scaleCap);

        _colorController = GetComponent<ColorController>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    /// <summary>
    /// Combine balls scale and mass.
    /// Prevent combine if total greater than either cap.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(!yarnAttributesSO.allowDamageCombine && (IsDamaged || !collision.gameObject.TryGetComponent(out IDamageable hitDamage) || hitDamage.isDamaged()))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<BallCombine>(out BallCombine hitBall) && hitBall.Color == Color)
        {
            if (!DecideBall(collision))
            {
                return;
            }

            Vector3 combinedScale = transform.localScale + hitBall.transform.localScale * yarnAttributesSO.scaleMultiplier;
            float combinedMass = _rigidBody.mass + collision.rigidbody.mass * yarnAttributesSO.massMultiplier;


            if (transform.localScale.x >= _scaleVectorCap.x * 0.99f)// || _rigidBody.mass >= _massCap)
            {
                return;            
            }

            Destroy(collision.gameObject);
        
            //Combine / absorb the mass
            transform.localScale = Vector3.Min(combinedScale, _scaleVectorCap);
            _rigidBody.mass = Mathf.Min(combinedMass, yarnAttributesSO.massCap);

            if (transform.localScale == _scaleVectorCap)// || _rigidBody.mass == _massCap)
            {
                OnMaxSize.Invoke();
            }

            OnCombine.Invoke();

            AkSoundEngine.PostEvent(YarnCombineSound, gameObject);

        }
    }

    private bool DecideBall(Collision hitBall)
    {
        //localScale.x is float rounded i.e. returns false positive when !=
        if (transform.localScale != hitBall.transform.localScale)
            return transform.localScale.x > hitBall.transform.localScale.x;
        else
            return transform.position.y < hitBall.transform.position.y;
    }
}