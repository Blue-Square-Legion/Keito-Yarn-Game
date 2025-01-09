using UnityEngine;

public class IceCollision : MonoBehaviour
{
    [SerializeField] private GameObject iceBall;
    [SerializeField] private GameObject normalBall;

    private Rigidbody iceRB;
    private Rigidbody normalRB;

    void Start() {
        iceRB = iceBall.GetComponent<Rigidbody>();
        normalRB = normalBall.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        otherRB.drag = iceRB.drag;
        otherRB.GetComponentInParent<Rigidbody>().angularDrag = iceRB.angularDrag;
    }

    void OnTriggerExit(Collider other) {
        Rigidbody otherRB = other.GetComponentInParent<Rigidbody>();
        otherRB.drag = normalRB.drag;
        otherRB.GetComponentInParent<Rigidbody>().angularDrag = normalRB.angularDrag;
    }
}
