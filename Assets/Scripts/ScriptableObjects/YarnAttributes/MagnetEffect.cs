using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    [SerializeField] private RaycastHit[] hits;
    [SerializeField] private List<GameObject> yarnInRadius = new();
    [SerializeField] private float _magballRadius;
    [SerializeField] private float _pullForce, _pushForce;
    public LayerMask layerMask;
    private Renderer _myRenderer;
    [SerializeField] private bool _launched;
    public MagnetEffect(float magRadius, float pullForce, float pushForce) 
    {
        _magballRadius = magRadius;
        _pullForce = pullForce;
        _pullForce = pushForce;
    }
    // Start is called before the first frame update
    void Start()
    {
        _myRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!_launched)
            if (Input.GetButtonDown("Fire1"))
                _launched = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_launched) {
            hits = Physics.SphereCastAll(gameObject.transform.position, _magballRadius, Vector3.forward, 1f, layerMask);
            yarnInRadius.Clear();
            foreach (RaycastHit yarnHit in hits)
            {
                if (yarnHit.transform.gameObject != gameObject)
                {
                    yarnInRadius.Add(yarnHit.transform.gameObject);
                    //if (yarnHit.transform.gameObject.TryGetComponent<ColorController>(out ColorController hitYarnColor))
                    if (yarnHit.transform.gameObject.TryGetComponent<Renderer>(out Renderer hitYarnColor))
                    {
                        Rigidbody yarnBallHitRB = yarnHit.transform.gameObject.GetComponent<Rigidbody>();
                        Vector3 direction = yarnHit.transform.position - gameObject.transform.position;
                        direction = direction.normalized;
                        Debug.Log($"{hitYarnColor.material.GetColor("Yellow")}");
                        if (hitYarnColor.material.color.Equals(_myRenderer.material.color))
                        {
                            Debug.Log("Color is yellow. Pushing Ball");
                            yarnBallHitRB.velocity = direction * _pushForce;

                        }
                        else
                        {
                            if (Vector3.Distance(gameObject.transform.position, yarnHit.transform.position) > 1.5f)
                            {
                                Debug.Log("Color is not yellow. Pulling ball");
                                //yarnBallHitRB.AddForce(direction + Vector3.back * _pullForce);
                                yarnBallHitRB.velocity = -direction * _pullForce;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"{yarnHit.transform.gameObject} does not have a controller");
                    }
                }
            } 
        }
    }

    public IEnumerator CreateMagnetField() 
    {
        //Debug.Log("Coroutine started");
        /*yarnInRadius.Clear();
        hits = Physics.SphereCastAll(gameObject.transform.position, _magballRadius, Vector3.forward, 1f, layerMask);
        foreach (RaycastHit yarnHit in hits)
        {
            if (yarnHit.transform.gameObject != gameObject)
            {
                yarnInRadius.Add(yarnHit.transform.gameObject);
                if (yarnHit.transform.gameObject.TryGetComponent<ColorController>(out ColorController hitYarnColor))
                {
                    Rigidbody yarnBallHitRB = yarnHit.transform.gameObject.GetComponent<Rigidbody>();
                    Vector3 direction = yarnHit.transform.position - gameObject.transform.position;
                    direction = direction.normalized;
                    if (hitYarnColor.Color.Equals("Yellow"))
                    {
                        yarnBallHitRB.velocity = direction * _pushForce;
                        yield return null;

                    }
                    else
                    {
                        yarnBallHitRB.velocity = -direction * _pullForce;
                        yield return null;
                    }
                }
                else
                {
                    Debug.Log($"{yarnHit.transform.gameObject} does not have a controller");
                }
            }
        }*/
        yield return null;
    }

    public void OnDrawGizmos()
    {
        if (_launched)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, _magballRadius);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        yarnInRadius.Remove(collision.gameObject);
    }
}
