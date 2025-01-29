using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage: Interaction
{
    // [SerializeField] private string soundTag;

    protected override void EndInteract(GameObject obj) {}

    protected override void Interact(GameObject obj) {
        BallDamage damage = obj.GetComponent<BallDamage>();
        if (!damage) return;

        damage.Damage();
        // AkSoundEngine.PostEvent(soundTag, gameObject);
    }
}
