using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interaction
{
    [SerializeField] private List<GameObject> switchables;

    protected override void Interact(GameObject obj) {
        foreach (GameObject switchable in switchables) {
            switchable.GetComponent<Switchable>()?.Switch();
        }
    }

    protected override void EndInteract(GameObject obj) {}
}
