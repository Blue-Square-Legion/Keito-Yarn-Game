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
    private SphereCollider _collider;
    private Vector3 _scaleVectorCap;
    private ColorController _colorController;

    public string YarnCombineSound = "Play_Yarn_Combine";

    public Color Color => _renderer.material.color;
    public bool IsDamaged => _colorController.isDamaged();

    [HideInInspector] public bool _colorAlreadyChanged = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<SphereCollider>();
        _colorController = GetComponent<ColorController>();

        if (yarnAttributesSO)
        {
            _colorController.YarnAttributes = yarnAttributesSO;

            _rigidBody.mass = yarnAttributesSO.mass;

            InitializeYarnBall();
        }
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    /// <summary>
    /// Initializes the yarn balls stats when it is spawned and when the ball merges with a ball that it can merge with.
    /// </summary>
    public void InitializeYarnBall()
    {
        _scaleVectorCap = new Vector3(yarnAttributesSO.scaleCap, yarnAttributesSO.scaleCap, yarnAttributesSO.scaleCap);
        _collider.material.bounciness = yarnAttributesSO.bounciness;

        SetColor(yarnAttributesSO.color.Color);
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

        if (collision.gameObject.TryGetComponent(out BallCombine hitBall) && hitBall.yarnAttributesSO == yarnAttributesSO)
        {
            if (!DecideBall(collision))
            {
                return;
            }

            CombineBalls(collision, hitBall);
        } else if(FindObjectOfType<GameManager>()._challengeMode && transform.localScale == hitBall.transform.localScale)
        {
            // If the thrown ball can merge with the struck ball and both of their colors have not changed, merge both together and change
            // the balls color.
            if (yarnAttributesSO.mergableBalls.Contains(hitBall.yarnAttributesSO) && !hitBall._colorAlreadyChanged && !_colorAlreadyChanged)
            {
                foreach (ColorCombinationSO color in yarnAttributesSO.acceptableCombinations)
                {
                    if (color.AcceptableCombination(yarnAttributesSO, hitBall.yarnAttributesSO))
                    {
                        // Set this yarn ball to the color based on the acceptable combinations list
                        yarnAttributesSO = color.newYarnBall;

                        GetComponent<MeshRenderer>().material = yarnAttributesSO.color.YarnPrefab.GetComponent<MeshRenderer>().material;

                        // Set the color for the yarn trail to the yarn attribute SO color
                        GetComponent<TrailRenderer>().startColor = yarnAttributesSO.color.Color;

                        InitializeYarnBall();

                        _colorAlreadyChanged = true;
                        hitBall._colorAlreadyChanged = true;

                        CombineBalls(collision, hitBall);
                        break;
                    }
                }
            }
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

    /// <summary>
    /// Combines the two balls based on their scale
    /// </summary>
    /// <param name="collision">The collision of the struck ball.</param>
    /// <param name="hitBall">The thrown ball that the player hit.</param>
    private void CombineBalls(Collision collision, BallCombine hitBall)
    {
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