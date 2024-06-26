using System.Collections.Generic;
using UnityEngine;

public class MusicColorSwitcher : MonoBehaviour
{

    [SerializeField, Tooltip("Color SO that will trigger a switch to rock variation")] private List<ColorSO> _colorsSwitchToRock = new();
    [SerializeField, Tooltip("Color SO that will trigger a switch to disco variation")] private List<ColorSO> _colorsSwitchToDisco = new();
    [SerializeField, Tooltip("Color SO that will trigger a switch to children variation")] private List<ColorSO> _colorsSwitchToChildren = new();


    /// <summary>
    /// Switch music based on provided color
    /// </summary>
    /// <param name="color">The new yarn ball color</param>
    public void SwitchMusic(ColorSO color)
    {
        if (_colorsSwitchToRock.Contains(color))
        {
            // TODO: Switch to rock music
        }
        else if (_colorsSwitchToDisco.Contains(color))
        {
            // TODO: Switch to disco music
        }
        else if (_colorsSwitchToChildren.Contains(color))
        {
            // TODO: Switch to children music
        }
        else
        {
            // TODO: Do one of the following:
            //       1) Switch to original level music
            //       2) Do nothing
        }
    }
}