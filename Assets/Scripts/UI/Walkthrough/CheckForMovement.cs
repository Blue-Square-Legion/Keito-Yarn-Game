using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class CheckForMovement : MonoBehaviour
{
    [SerializeField] private Image ButtonW, ButtonA, ButtonS, ButtonD, ButtonF1, ButtonF2;
    [SerializeField] private Sprite arrow, check;

    [SerializeField] private float moveDistance;
    [SerializeField] private Transform cameraPos;

    private float difLeft, difRight, difUp, difDown;
    private bool _leftDone, _rightDone, _upDone, _downDone;
    private Vector3 _cameraLastPos;

    private int _checkList, _whichF;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform;
        _cameraLastPos = cameraPos.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckUp();
        CheckDown();
        CheckLeft();
        CheckRight();
        CheckListComplete();
        UpdateLastPos();
    }
    void CheckUp()
    {
        if (!_upDone)
        {
            if (Input.GetAxis("Vertical") >= 0)
            {
                difUp += Input.GetAxis("Vertical");
                if (difUp >= moveDistance)
                {
                    ButtonW.sprite = check;
                    _checkList++;
                    _upDone = true;
                }
            }
        }
    }
    void CheckDown()
    {
        if (!_downDone)
        {
            if (Input.GetAxis("Vertical") <= 0)
            {
                difDown += Mathf.Abs(Input.GetAxis("Vertical"));
                if (difDown >= moveDistance)
                {
                    ButtonS.sprite = check;
                    ButtonS.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _checkList++;
                    _downDone = true;
                }
            }
        }
    }
    void CheckLeft()
    {
        if (!_leftDone)
        {
            if (Input.GetAxis("Horizontal") <= 0)
            {
                difLeft += Mathf.Abs(Input.GetAxis("Horizontal"));
                if (difLeft >= moveDistance)
                {
                    ButtonA.sprite = check;
                    ButtonA.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _checkList++;
                    _leftDone = true;
                }
            }
        }
    }
    void CheckRight()
    {
        if (!_rightDone)
        {
            if (Input.GetAxis("Horizontal") >= 0)
            {
                difRight += Input.GetAxis("Horizontal");
                if (difRight >= moveDistance)
                {
                    ButtonD.sprite = check;
                    ButtonD.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _checkList++;
                    _rightDone = true;
                }
            }
        }
    }

    void CheckListComplete() 
    {
        if (_checkList == 6)//One for Up, Down, Left, Right, and two Flips. Six total 
        {
            gameObject.SetActive(false);
            RevisedWalkthrough.RW.hasMoved = true;//Sets the movement bool to true
            RevisedWalkthrough.RW.NextSlide();//Changes the slide
        }
    }

    void UpdateLastPos() {
        _cameraLastPos = cameraPos.position;
    }

    private void OnEnable()
    {
        InputManager.Input.Player.Focus.performed += Focus_performed;
    }

    private void OnDisable()
    {
        InputManager.Input.Player.Focus.performed -= Focus_performed;
    }

    private void Focus_performed(InputAction.CallbackContext obj)
    {
        _whichF++;
        if (_whichF == 1)
        {
            ButtonF1.sprite = check;
            ButtonF1.rectTransform.localPosition = new Vector3(ButtonF1.rectTransform.localPosition.x - 50f, ButtonF1.rectTransform.localPosition.y, 0);
            _checkList++;
        }
        else if (_whichF == 2)
        {
            ButtonF2.sprite = check;
            ButtonF2.transform.rotation = Quaternion.Euler(0, 0, 0);
            ButtonF2.rectTransform.localPosition = new Vector3(ButtonF2.rectTransform.localPosition.x + 50f, ButtonF2.rectTransform.localPosition.y, 0);
            _checkList++;
        }
    }
}
