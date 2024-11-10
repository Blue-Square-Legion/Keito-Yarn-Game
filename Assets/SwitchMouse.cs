using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchMouse : MonoBehaviour
{
    private float timer = 1.5f;
    [SerializeField] private Image mouse;
    [SerializeField] private Sprite mouseClick, mouseUnclick;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (mouse.sprite.Equals(mouseClick))
            {
                mouse.sprite = mouseUnclick;
                timer = 1.5f;
            }
            else
            {
                mouse.sprite = mouseClick;
                timer = 1.5f;
            }
        }
    }
}
