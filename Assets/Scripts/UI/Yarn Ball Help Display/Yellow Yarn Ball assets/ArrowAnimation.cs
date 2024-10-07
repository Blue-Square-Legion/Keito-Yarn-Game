using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    public string arrowAnimation;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void CallThisFromButton()
    {
        anim.Play(arrowAnimation);
    }
}
