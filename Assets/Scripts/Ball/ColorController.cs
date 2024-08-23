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

    /// <summary>
    ///  Just doing some testing with the red ball
    ///  </summary>
    private void Update()
    {
        if (gameObject == null)
            return;
        if (gameObject.GetComponent<YarnCollision>().isThrown && gameObject.GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.05f)
            {
                print(gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                gameObject.GetComponent<YarnCollision>().isThrown = false;
            }
        }
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
