using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    protected abstract void Interact(GameObject obj);

    protected abstract void EndInteract(GameObject obj);

    private void OnCollisionEnter(Collision collision) {
        Interact(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision) {
        EndInteract(collision.gameObject);
    }
}
