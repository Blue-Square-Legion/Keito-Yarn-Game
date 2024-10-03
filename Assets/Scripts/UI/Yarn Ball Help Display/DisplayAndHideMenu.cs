using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAndHideMenu : MonoBehaviour
{
    public Button enter;
    public Button exit;
    public GameObject pauseUI;
    public GameObject yarnBallUI;

    /* <Summary>
     * Enter button needs to hide the current Pause UI, and display Yarn Ball Viewer
     * Exit button needs to display pause UI and hide Yarn Ball Viewer
     */
    void Start()
    {
        Button btnOne = enter.GetComponent<Button>();
        btnOne.onClick.AddListener(yarnUIOn);
        Button btnTwo = exit.GetComponent<Button>();
        btnTwo.onClick.AddListener(yarnUIOff);
    }
    void yarnUIOn()
    {
        pauseUI.SetActive(false);
        Debug.Log("pause UI is off");
        yarnBallUI.SetActive(true);
        Debug.Log("yarn ball help ui is on");
    }

    void yarnUIOff()
    {
        yarnBallUI.SetActive(false);
        Debug.Log("yarn ball help ui is off");
        pauseUI.SetActive(true);
        Debug.Log("pause UI is on");
    }
}
