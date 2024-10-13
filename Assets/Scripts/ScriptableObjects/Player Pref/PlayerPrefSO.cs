using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Pref", menuName = "SO/Player Pref")]
public class PlayerPrefSO : ScriptableObject
{
    public enum Key
    {
        Master_Volume,
        Music_Volume,
        Sound_Volume,
        Mouse_Sensitivity,
        Camera_Sensitivity
    }

    public Key currKey;
}
