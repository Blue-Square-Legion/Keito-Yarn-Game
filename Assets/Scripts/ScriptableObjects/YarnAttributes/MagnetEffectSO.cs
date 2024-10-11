using UnityEngine;

[CreateAssetMenu(fileName = "MagnetEffect", menuName = "Effects/MagnetEffect")]
public class MagnetEffectSO : YarnBallEffectSO
{
    [SerializeField] private float _magballRadius;
    [SerializeField] private float _pullForce, _pushForce;
    [SerializeField] private float _effectDuration = 5f; // Duration for which the effect is active
    public LayerMask layerMask;
    [SerializeField] private GameObject particlesPrefab;
    private GameObject particlesObj;


    public override void CreateEffect(GameObject ball, Transform target = null)
    {
        Debug.Log("Magnet Field Created");

        // Ensure the ball has a MagnetEffectHandler
        MagnetEffectHandler handler = ball.GetComponent<MagnetEffectHandler>();
        if (handler == null)
        {
            handler = ball.AddComponent<MagnetEffectHandler>();
        }

        Renderer ballRenderer = ball.GetComponent<Renderer>();
        handler.Initialize(_magballRadius, _pullForce, _pushForce, _effectDuration, layerMask, ballRenderer);
        particlesObj = Instantiate(particlesPrefab, ball.transform);
        particlesObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null)
    {
        // Apply any immediate effects or additional logic here, if necessary
    }

    public override bool ShouldApplyOnCollision()
    {
        return false; // This effect isn't triggered by collisions, but by the CreateEffect method
    }
}
