using System.Collections;
using UnityEngine;

public class MagnetEffectHandler : MonoBehaviour
{
    private Coroutine magnetEffectCoroutine;

    public void Initialize(float radius, float pullForce, float pushForce, float duration, LayerMask layerMask, Renderer ballRenderer)
    {
        if (magnetEffectCoroutine != null)
        {
            StopCoroutine(magnetEffectCoroutine);
        }
        magnetEffectCoroutine = StartCoroutine(ApplyMagnetEffect(radius, pullForce, pushForce, duration, layerMask, ballRenderer));
    }

    private IEnumerator ApplyMagnetEffect(float radius, float pullForce, float pushForce, float duration, LayerMask layerMask, Renderer ballRenderer)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject != gameObject && collider.gameObject.TryGetComponent<Renderer>(out Renderer hitYarnColor))
                {
                    Rigidbody yarnBallHitRB = collider.gameObject.GetComponent<Rigidbody>();
                    Vector3 direction = (collider.transform.position - transform.position).normalized;

                    if (hitYarnColor.material.color.Equals(ballRenderer.material.color))
                    {
                        if (Vector3.Distance(transform.position, collider.transform.position) > 1.5f)
                        {
                            yarnBallHitRB.velocity = -direction * pullForce; // Attract
                        }
                    }
                    else
                    {
                        yarnBallHitRB.velocity = direction * pushForce; // Repel
                    }
                }
            }

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        Debug.Log("Magnet Effect Ended");
    }
}
