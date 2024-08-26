using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour, IDamageable, IRepairable
{
    [SerializeField] private YarnAttributesSO yarnBallAttributes;

    private bool _isDamaged = false;

    public YarnAttributesSO YarnAttributes { get { return yarnBallAttributes; } set { yarnBallAttributes = value; } }
    public ColorSO Color => yarnBallAttributes.color;
    private Renderer _render;

    private void Start()
    {
        _render = gameObject.GetComponent<Renderer>();
    }

   
    private void SetColor(float modifier = 1f)
    {
        _render.material.color = yarnBallAttributes.color.Color * modifier;
    }

    public void Damage()
    {
        if (!_isDamaged)
        {
            _isDamaged = true;
            SetColor(0.5f);
        }        
    }

    public void Repair()
    {
        _isDamaged = false;
        SetColor();
    }

    public bool isDamaged()
    {
        return _isDamaged;
    }
}
