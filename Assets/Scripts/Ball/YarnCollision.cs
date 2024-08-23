using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YarnCollision : MonoBehaviour
{
    public const string YARN_TAG = "Yarn";
    public const string CAT_TAG = "Cat";
    private bool hasCollidedBefore = false;
    public string YarnCollisionSound = "Play_Yarn_Hit";
    //public string CatYarnCollisionSound = "Play_Cat_Purr";
    public string YarnMergeCollisionSound = "Play_Yarn_Merge";
    public string MaxSizeGlowSound = "Play_MaxSizeGlow";
    public string fanEvent = "Play_Fan";
    public string stopFanEvent = "Stop_Fan";
    bool isFanOn = false;
    public bool isThrown = false;

    /// <summary>
    /// When yarn first collides with another object
    /// </summary>
    /// <param name="other">The other object</param>
    private void OnCollisionEnter(Collision other)
    {
        bool isYarn = other.gameObject.CompareTag(YARN_TAG);
        // TODO: Find cat component/tag
        bool isCat = other.gameObject.CompareTag(CAT_TAG);
        /*bool isCat = false;*/
        PostYarnCollisionEvent();
        // If yarn collides with another yarn
        if (isYarn)
        {
            if (IsOtherSameColor(other))
            {
                // TODO: Yarn combining SFX
                // PostYarnMergeCollisionEvent();
            }
            else
            {
                // TODO: Yarn-yarn collision SFX
                ApplyEffectBasedOnColor(other);
            }
        }
        // If collided with cat
        else if (isCat)
        {
            // TODO: Yarn-cat collision SFX
            //PostCatYarnCollisionEvent(); //Now handled on Cat prefab > CatSound
        }
        // If first collision
        else if (!hasCollidedBefore)
        {
            hasCollidedBefore = true;
          
        }
        // otherwise
        else
        {
            
        }
    }

    private bool IsOtherSameColor(Collision other) {
        if(GetThisBallColor() == GetOtherBallColor(other)) {
            return true;
        }
        return false;
    }

    private ColorSO.BallColor GetThisBallColor() {
        Renderer thisRenderer = GetComponent<Renderer>();
        Color thisColor = thisRenderer.material.color;
        return GetBallColor(thisColor);
    }

    private ColorSO.BallColor GetOtherBallColor(Collision other) {
        if (other.gameObject.TryGetComponent(out Renderer otherRenderer))
        {
            Color otherColor = otherRenderer.material.color;
            return GetBallColor(otherColor);
        }
        Debug.LogError("other color not found");
        return ColorSO.BallColor.Default;
    }

    private ColorSO.BallColor GetBallColor(Color color) {
        if (color.r > .9 && color.g < .4 && color.b < .4) {
            return ColorSO.BallColor.Red;
        } else if (color.b > .9 && color.r < .4 && color.g < .4) {
            return ColorSO.BallColor.Blue;
        } else if (color.g > .9 && color.r < .4 && color.b < .4) {
            return ColorSO.BallColor.Green;
        }
        Debug.LogError("Couldn't identify Ball Color");
        return ColorSO.BallColor.Default;
    }

    private void ApplyEffectBasedOnColor(Collision other)
    {
        ColorSO.BallColor thisBallColor = GetThisBallColor();
        //Debug.Log(thisBallColor);

        if (thisBallColor == ColorSO.BallColor.Red && isThrown)
        {
            ExcessiveForceEffect xfEffect = new ExcessiveForceEffect(other.gameObject);
            xfEffect.ApplyEffect();
        }
        else if (thisBallColor == ColorSO.BallColor.Green)
        {
            Debug.Log("will apply sticky effect");
            StickyEffect stEffect = new StickyEffect(transform, other.transform);
            stEffect.ApplyEffect();
        }
    }

    public void OnMaxSize()
    {
        //TODO - Add max size sound
        AkSoundEngine.PostEvent(MaxSizeGlowSound, gameObject);

    }

    public void PostYarnCollisionEvent()
    {
        // Check if the event name is valid
        if (!string.IsNullOrEmpty(YarnCollisionSound))
        {
            // Post the Wwise yarn collision event by name
            AkSoundEngine.PostEvent(YarnCollisionSound, gameObject);
        }
        else
        {
            Debug.LogError("Yarn Collision Sound event name is not specified!");
        }
    }
/*    public void PostCatYarnCollisionEvent()
    {
        // Check if the event name is valid
        if (!string.IsNullOrEmpty(CatYarnCollisionSound))
        {
            // Post the Wwise yarn collision event by name
            AkSoundEngine.PostEvent(CatYarnCollisionSound, gameObject);
        }
        else
        {
            Debug.LogError("Cat-Yarn Collision Sound event name is not specified!");
        }
    }*/
    public void PostYarnMergeCollisionEvent()
    {
        // Check if the event name is valid
        if (!string.IsNullOrEmpty(YarnMergeCollisionSound))
        {
            // Post the Wwise yarn collision event by name
            AkSoundEngine.PostEvent(YarnMergeCollisionSound, gameObject);
        }
        else
        {
            Debug.LogError("Cat-Yarn Collision Sound event name is not specified!");
        }
    }
    public void PayFan()
    {
        if (!isFanOn) {
            AkSoundEngine.PostEvent(fanEvent, gameObject);
            isFanOn = true;
        }
        
        
    }



    public void StopFan()
    {
        if (isFanOn)
        {
            AkSoundEngine.PostEvent(stopFanEvent, gameObject);
            isFanOn = false;
        }
       

    }
}
