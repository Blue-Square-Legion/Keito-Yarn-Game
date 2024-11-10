using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RevisedWalkthrough : MonoBehaviour
{
    //This script will be used to make sure the following events have been triggered. Once triggered, they will then call the next slide and wait for the next event.
    public bool hasMoved, firstYarnCollision, firstYarnMerge, fistBallAccepted;
    public SlingShot sling;
    ///First: Has the player used the WASD/F keys to move around enough. --- Done
    ///Second: has the player made a yarn ball collide with the cat --- Done
    ///Third: Has the player merged a yarn ball to make a larger one yet?
    ///Fourth: Has the player given the newly enlarged yarn ball to the cat
    /// </summary>

    public StaticWalkThroughManager _revisedWalkthrough;
    public static RevisedWalkthrough RW;

    private void Start()
    {
        if(RW == null)
            RW = this;
        sling.SetRemainingYarn(0);//This will prevent the player from firing any yarn before they've completed the first slide
    }

    public void NextSlide() 
    {
        _revisedWalkthrough.RunNextSlide();
        sling.SetUnlimitedYarn(true);//Added this to enable the player to launch yarn again like normal
    }

}