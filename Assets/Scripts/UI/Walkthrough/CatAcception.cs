using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAcception : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private CatYarnInteraction catYarn;
    [SerializeField] private YarnAttributesSO redYarn;
    // Start is called before the first frame update

    private void Start()
    {
        gameManager.AddNewColor(redYarn);
        //gameManager.EnforceCatColor(null);
    }

    public void AssignCat(CatYarnInteraction newCat) 
    {
        catYarn = newCat;
        AddCatYarn();
    }

    private void CatAcceptYarn(float unUsed, bool notReq)
    {
        RevisedWalkthrough.RW.NextSlide();
        RemoveCatYarn();
        //gameManager.
    }

    private void AddCatYarn() 
    {
        catYarn?.OnCatScored.AddListener(CatAcceptYarn);
    }
    private void RemoveCatYarn()
    {
        catYarn?.OnCatScored.RemoveListener(CatAcceptYarn);
    }
}
