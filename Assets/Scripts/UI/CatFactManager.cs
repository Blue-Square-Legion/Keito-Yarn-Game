using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFactManager : MonoBehaviour
{
    [SerializeField] private GameObject _catFactPrefab;
    public CatfactSeparation _catfactSeparation;

    private void Start()
    {
        Collectable[] list = FindObjectsOfType<Collectable>();

        foreach(Collectable collectable in list)
        {
            collectable.OnCollect.AddListener(SpawnCatFact);
        }
    }

    public void SpawnCatFact(CatFactSO data)
    {
        GameObject go = Instantiate(_catFactPrefab, transform);
        CatFactToast toast = go.GetComponent<CatFactToast>();
        toast.SetUp(data);
        //updateUI goes here, in order to put the catfact image/gameobject in a list and move them when needed.
        _catfactSeparation.updateUI(go);
    }

    public void SpawnCatFact(CollectableSO data)
    {
        SpawnCatFact(data.CatFact);
    }
}
