using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switchable : MonoBehaviour
{
    public virtual void Switch() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
