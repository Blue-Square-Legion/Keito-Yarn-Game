using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YarnBallExamples : MonoBehaviour
{
    public Button blue;
    public Button red;
    public Button green;
    //public Button wildcard;
    public Button cyan;
    public Button yellow;
    public Button back;
    public GameObject blueImage;
    public GameObject redImage;
    public GameObject greenImage;
    //public GameObject wildcardImage;
    public GameObject changeImage;
    public GameObject yellowImage;
    // Start is called before the first frame update
    void Start()
    {
        Button btnBlue = blue.GetComponent<Button>();
        btnBlue.onClick.AddListener(blueHelp);
        Button btnRed = red.GetComponent<Button>();
        btnRed.onClick.AddListener(redHelp);
        Button btnGreen = green.GetComponent<Button>();
        btnGreen.onClick.AddListener(greenHelp);
        //Button btnWild = wildcard.GetComponent<Button>();
        //btnWild.onClick.AddListener(wildcardHelp);
        Button btnChange = cyan.GetComponent<Button>();
        btnChange.onClick.AddListener(cyanHelp);
        Button btnYellow = yellow.GetComponent<Button>();
        btnYellow.onClick.AddListener(yellowHelp);
        Button btnBack = back.GetComponent<Button>();
        btnBack.onClick.AddListener(BackMenu);
    }
    void blueHelp() 
    {
        yarnMenu(1);
    }
    void redHelp()
    {
        yarnMenu(2);
    }
    void greenHelp()
    {
        yarnMenu(3);
    }
    //void wildcardHelp()
    //{
    //    yarnMenu(3);
    //}
    void cyanHelp()
    {
        yarnMenu(4);
    }
    void yellowHelp()
    {
        yarnMenu(5);
    }
    void BackMenu()
    {
        yarnMenu(0);
    }
    private void yarnMenu(int select)
    {
        switch(select)
        {
            case 0:
                Debug.Log("Back selected");
                blueImage.SetActive(false);
                redImage.SetActive(false);
                greenImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 1:
                Debug.Log("Red ball selected");
                blueImage.SetActive(true);
                redImage.SetActive(false);
                greenImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 2:
                Debug.Log("Green ball selected");
                blueImage.SetActive(false);
                redImage.SetActive(true);
                greenImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 3:
                Debug.Log("Wildcard ball selected");
                blueImage.SetActive(false);
                redImage.SetActive(false);
                greenImage.SetActive(true);
                changeImage.SetActive(false);
                yellowImage.SetActive(false);
                break;
            case 4:
                Debug.Log("Change ball selected");
                blueImage.SetActive(false);
                redImage.SetActive(false);
                greenImage.SetActive(false);
                changeImage.SetActive(true);
                yellowImage.SetActive(false);
                break;
            case 5:
                Debug.Log("Yellow ball selected");
                blueImage.SetActive(false);
                redImage.SetActive(false);
                greenImage.SetActive(false);
                changeImage.SetActive(false);
                yellowImage.SetActive(true);
                break;

        }
    }
}
