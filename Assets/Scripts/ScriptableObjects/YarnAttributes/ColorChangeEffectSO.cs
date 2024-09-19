using UnityEngine;

[CreateAssetMenu(fileName = "ColorChangeEffect", menuName = "Effects/ColorChangeEffect")]
public class ColorChangeEffectSO : YarnBallEffectSO
{
    [SerializeField] private GameObject newBallPrefab;
    public override void CreateEffect(GameObject ball, Transform target = null) 
    {
    }

    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody)
    {
        if (ballRigidbody != null && targetRigidbody != null)
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
                }

                newBall.transform.localScale = originalScale;

                Debug.Log("Replaced target with new color ball: " + newBall.name);
            }
            else
            {
                Debug.LogError("New ball prefab is not assigned.");
            }
        }
        else
        {
            Debug.LogError("ColorChange effect cannot be applied: either ballRigidbody or targetRigidbody is missing.");
        }
    }
}
