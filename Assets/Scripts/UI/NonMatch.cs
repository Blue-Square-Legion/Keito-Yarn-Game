using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonMatch : MonoBehaviour
{
    [SerializeField] private GameObject catSprite;
    [SerializeField] private GameObject yarnSprite;
    private Image _catColor;
    private Image _yarnColor;

    public void SetColors(ColorSO cat, ColorSO yarn)
    {
        _catColor.color = cat.YarnMaterial.color;
        _yarnColor.color = yarn.YarnMaterial.color;
    }

    void Awake() 
    {
        _catColor = catSprite.GetComponent<Image>();
        _yarnColor = yarnSprite.GetComponent<Image>();
    }
}
