using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MangetEffect", menuName = "Effects/MagnetEffect")]
public class MagnetSO : YarnBallEffectSO
{
    private RaycastHit[] hits;
    //private List<GameObject> yarnInRadius;
    [SerializeField] private float _magballRadius;
    [SerializeField] private float _pullForce, _pushForce;
    public LayerMask layerMask;
    //public 
    GameObject _ball;

    private void Update() 
    {
        //yarnInRadius.Clear();
    }

    //ball is the game object the script is attached to. Target is the other ball the game object collides with. Not necesary for this script
    public override void CreateEffect(GameObject ball, Transform target = null)
    {
        _ball = ball;
    }
    public override void ApplyEffect(GameObject ball, Rigidbody ballRigidbody, Rigidbody targetRigidbody = null)
    {
        hits = Physics.SphereCastAll(ball.transform.position, _magballRadius, Vector3.forward, 1f, layerMask);
    }
    public override bool ShouldApplyOnCollision()
    {
        return true;
    }

    private void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_ball.transform.position, _magballRadius);
    }

    IEnumerator ApplyMagnetism() 
    {
        foreach (RaycastHit yarnInRadius in hits) 
        {
            GameObject yarnBallHit = yarnInRadius.transform.gameObject;
            if (yarnBallHit.GetComponent<Yarn>)
            { 
            
            }
        }
        yield return null;
    }
}
