using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAcception : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private CatYarnInteraction catYarn;
    [SerializeField] private ColorSO nextCatColor;
    [SerializeField] private YarnAttributesSO redYarn;
    // Start is called before the first frame update

    private void Start()
    {
        gameManager.EnforceCatColor(nextCatColor);//This is to make the next cat spawn blue because the player will have more blue yarns before a red one will appear in the list
        RevisedWalkthrough.RW.gameManager = gameManager;
    }

    public void AssignCat(CatYarnInteraction newCat) 
    {
        catYarn = newCat;
        AddCatYarn();
    }

    private void CatAcceptYarn(float unUsed, bool notReq)
    {
        RevisedWalkthrough.RW.NextSlide();
        RemoveCatYarn();//Cat gets destroyed when the yarn collides so no need to remove the listener.
    }

    private void AddCatYarn() 
    {
        catYarn?.OnCatScored.AddListener(CatAcceptYarn);
        Debug.Log($"Adding Listener to {catYarn.name}");
    }
    private void RemoveCatYarn()
    {
        gameManager.AddNewColor(redYarn, 1);
        catYarn?.OnCatScored.RemoveListener(CatAcceptYarn);
        Debug.Log("Removing Listener from CatYarn");
    }
}
