using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RevisedWalkthrough : MonoBehaviour
{
    //This script will be used to make sure the following events have been triggered. Once triggered, they will then call the next slide and wait for the next event.
    public bool hasMoved, firstYarnCollision, firstYarnMerge, fistBallAccepted;
    ///First: Has the player used the WASD/F keys to move around enough.
    ///Second: has the player made a yarn ball collide with the cat? Should be a yarn of the wrong size <summary>
    ///Third: Has the player merged a yarn ball to make a larger one yet?
    ///Fourth: Has the player given the newly enlarged yarn ball to the cat
    /// </summary>

    public StaticWalkThroughManager _revisedWalkthrough;
    public static RevisedWalkthrough RW;

    private void Start()
    {
        if(RW == null)
            RW = this;
    }

    public void NextSlide() 
    {
        _revisedWalkthrough.RunNextSlide();
    }
}