using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColorSO;


public enum SignPostTypes
{
    HappyCat,
    AngryCat,
    BlueYarn,
    GreenYarn,
    RedYarn,
    WrongColor,
    TooSmall
}


public class SignManager : MonoBehaviour
{
    [SerializeField] private GameObject _signPrefab;
    [SerializeField] private float _animationTime = 0.5f;
    [SerializeField] private float _displayTime = 2f;

    [Space(5)]

    [SerializeField] private Sprite _angryCat;
    [SerializeField] private Sprite _happyCat;
    [SerializeField] private Sprite _redYarn;
    [SerializeField] private Sprite _greenYarn;
    [SerializeField] private Sprite _blueYarn;
    [SerializeField] private Sprite _blankSign;

    private SignPostNotification _previous;

    private void Start()
    {
        SignPostNotification preset = _signPrefab.GetComponent<SignPostNotification>();
        preset.AnimationTime = _animationTime;
        preset.DisplayTime = _displayTime;
    }

    public void Open(SignPostTypes type)
    {
        switch (type)
        {
            case SignPostTypes.AngryCat: Open(_angryCat); break;
            case SignPostTypes.HappyCat: Open(_happyCat); break;
            case SignPostTypes.BlueYarn: Open(_blueYarn); break;
            case SignPostTypes.GreenYarn: Open(_greenYarn); break;
            case SignPostTypes.RedYarn: Open(_redYarn); break;
        }
    }

    public void Open(Sprite sprite)
    {
        if(_previous != null)
        {
            _previous?.Close();
        }

        _previous = CreateSign(sprite);
    }

    public void Open(Sprite sprite, RejectType reason, ColorSO yarnColor)
    {
        if (_previous != null)
        {
            _previous?.Close();
        }

        _previous = CreateSign(sprite, reason, yarnColor);
    }

    public void Open(RejectType reason, ColorSO yarncolor)
    {
        Open(_blankSign, reason, yarncolor);
    }

    private SignPostNotification CreateSign(Sprite sprite)
    {
        GameObject go = Instantiate(_signPrefab, transform);
        SignPostNotification sign = go.GetComponent<SignPostNotification>();
        sign.Open(sprite);

        return sign;
    }

    private SignPostNotification CreateSign(Sprite sprite, RejectType reason, ColorSO yarnColor)
    {
        GameObject go = Instantiate(_signPrefab, transform);
        SignPostNotification sign = go.GetComponent<SignPostNotification>();
        sign.Open(sprite, reason, yarnColor);

        return sign;
    }
}
