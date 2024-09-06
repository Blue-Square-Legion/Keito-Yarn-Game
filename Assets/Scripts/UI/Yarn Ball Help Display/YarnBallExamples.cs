using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YarnBallExamples : MonoBehaviour
{
    public Button red;
    public Button green;
    public Button wildcard;
    public Button change;
    public Button yellow;
    public GameObject redImage;
    public GameObject greenImage;
    public GameObject wildcardImage;
    public GameObject changeImage;
    public GameObject yellowImage;
    // Start is called before the first frame update
    void Start()
    {
        Button btnRed = red.GetComponent<Button>();
        btnRed.onClick.AddListener(redHelp);
        Button btnGreen = green.GetComponent<Button>();
        btnGreen.onClick.AddListener(greenHelp);
        Button btnWild = wildcard.GetComponent<Button>();
        btnWild.onClick.AddListener(wildcardHelp);
        Button btnChange = change.GetComponent<Button>();
        btnChange.onClick.AddListener(changeHelp);
        Button btnYellow = yellow.GetComponent<Button>();
        btnYellow.onClick.AddListener(yellowHelp);
    }
    void redHelp()
    {
        yarnMenu(1);
    }
    void greenHelp()
    {
        yarnMenu(2);
    }
    void wildcardHelp()
    {
        yarnMenu(3);
    }
    void changeHelp()
    {
        yarnMenu(4);
    }
    void yellowHelp()
    {
        yarnMenu(5);
    }
    private void yarnMenu(int select)
    {
        switch(select)
        {
            case 1:
                Debug.Log("Red ball selected");
                redImage.SetActive(true);
                greenImage.SetActive(false);
                wildcardImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 2:
                Debug.Log("Green ball selected");
                redImage.SetActive(false);
                greenImage.SetActive(true);
                wildcardImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 3:
                Debug.Log("Wildcard ball selected");
                redImage.SetActive(false);
                greenImage.SetActive(false);
                wildcardImage.SetActive(true);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 4:
                Debug.Log("Change ball selected");
                redImage.SetActive(false);
                greenImage.SetActive(false);
                wildcardImage.SetActive(false);
                changeImage.SetActive(true);
                yellowImage.SetActive(false);
                break;
            case 5:
                Debug.Log("Yellow ball selected");
                redImage.SetActive(false);
                greenImage.SetActive(false);
                wildcardImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(true);
                break;

        }
    }
}
