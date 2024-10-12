using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static ColorSO;

public class SignColorizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> Yarn = new List<Sprite>();
    [SerializeField] private List<Sprite> CrossedYarn = new();
    [SerializeField] private GameObject _wrongColor;
    [SerializeField] private GameObject _tooSmall;
    void Awake() 
    {
        
    }
    public void SetSign(RejectType reason, ColorSO yarn)
    {
        switch(reason)
        {
            case RejectType.Color:
                _wrongColor.SetActive(true);
                SetWrongColorsSign(yarn);
                break;
            case RejectType.Size:
                _tooSmall.SetActive(true);
                SetTooSmallSign(yarn);
                break;
        }
    }
    public void SetWrongColorsSign(ColorSO yarn)
    {
        Image sign = _wrongColor.GetComponent<Image>();
        if (yarn.Name == BallColor.Red)
        {
            sign.sprite = CrossedYarn[0];
        }
        else if (yarn.Name == BallColor.Green)
        {
            sign.sprite = CrossedYarn[1];
        }
        else if (yarn.Name == BallColor.Blue)
        {
            sign.sprite = CrossedYarn[2];
        }
        else if (yarn.Name == BallColor.Yellow)
        {
            sign.sprite = CrossedYarn[3];
        }
        else if (yarn.Name == BallColor.Sky_Blue)
        {
            sign.sprite = CrossedYarn[4];
        }
    }
    public void SetTooSmallSign(ColorSO yarn)
    {
        Image[] images = new Image[2];
        images = _tooSmall.GetComponentsInChildren<Image>();
        if (yarn.Name == BallColor.Red)
        {
            images[0].sprite = CrossedYarn[0];
            images[1].sprite = Yarn[0];
        }
        else if (yarn.Name == BallColor.Green)
        {
            images[0].sprite = CrossedYarn[1];
            images[1].sprite = Yarn[1];
        }
        else if (yarn.Name == BallColor.Blue)
        {
            images[0].sprite = CrossedYarn[2];
            images[1].sprite = Yarn[2];
        }
        else if (yarn.Name == BallColor.Yellow)
        {
            images[0].sprite = CrossedYarn[3];
            images[1].sprite = Yarn[3];
        }
        else if (yarn.Name == BallColor.Sky_Blue)
        {
            images[0].sprite = CrossedYarn[4];
            images[1].sprite = Yarn[4];
        }
    }
}
