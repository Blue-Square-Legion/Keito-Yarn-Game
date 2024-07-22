using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour, IDamageable, IRepairable
{
    [SerializeField] private YarnAttributesSO yarnBallAttributes;

    private bool _isDamaged = false;

    public ColorSO Color => yarnBallAttributes.color;
    private Renderer _render;
    private SphereCollider _collider;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _render = gameObject.GetComponent<Renderer>();
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();

        _collider.material.bounciness = yarnBallAttributes.bounciness;
        _rigidbody.mass = yarnBallAttributes.mass;
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
