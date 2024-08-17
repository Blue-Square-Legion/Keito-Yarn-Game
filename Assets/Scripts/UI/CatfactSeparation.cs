using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatfactSeparation : MonoBehaviour
{
    /*
     * Summary: This script allows the cat facts to spawn when another is up
     * the catfacts will be added to a list when spawned
     * they will then have the existing catfacts move down when another catfact spawns
     * the first one will not be affected until another catfact is up
     * probably wont matter, but if the top one happens to despawn early, the bottom one will move back up
     * Where the catfact spawns in, updateUI needs to happen
     */
    List<GameObject> catFactsList = new List<GameObject>();
    //the distance that the catfact will travel when another catfact spawns.
    /*
     * The current issue with this calculation is that it is not moving a distance of 1 to 1.
     * The distance moved is 1.2508 for 1. and even then its exponential or something???
     * I do not know how this is happening
     * 177 is the number that gives it a fair bit of distance. 
     * if there is a need to update this to make sure nothing breaks, update the moveDown method and vector2 math.
     */
    private float distance = -177;
    //distance 2 will always be the inverse of distance 1. can more than likely create automation in start.
    private float distance2 = 177;
    public void updateUI(GameObject catFactImage)
    {
        Debug.Log("Update UI has been called");
        //put move down code here
        moveDown();
        Debug.Log("All things in list moved down");
        //Probably do not need to set the gameobject to true
        //catFact.SetActive(true);
        //adds the cat fact to the list, list affected in moveDown.
        catFactsList.Add(catFactImage);
        //foreach (Image catFact in catFactsList) { Debug.Log(catFactImage.name); }
    }
    public void moveDown()
    {
        foreach (GameObject catFactImage in catFactsList)
        {
            //spits out whats in the list
            Debug.Log(catFactImage.name);
            //transforms the items in the list to move down.
            RectTransform catFactTransform = catFactImage.gameObject.GetComponent<RectTransform>();
            catFactTransform.position = new Vector2(catFactTransform.position.x, catFactTransform.position.y + distance);
            Debug.Log("The thing has moved down");
        }
    }
    //method below may be used for the destroy script. unsure.
    public void moveUp()
    {
        foreach (GameObject catFactImage in catFactsList)
        {
            Debug.Log(catFactImage.name);
            RectTransform catFactTransform = catFactImage.GetComponent<RectTransform>();
            catFactTransform.position = new Vector2(catFactTransform.position.x, catFactTransform.position.y + distance2);
            Debug.Log("The thing has moved up");
        }
    }
}
