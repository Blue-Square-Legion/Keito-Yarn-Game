using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMerge : MonoBehaviour
{
    [SerializeField] private List<ColorController> balls = new();
    public bool stopMerger, allowMerger;
    // Start is called before the first frame update
    void Start()
    {
        //stopMerger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopMerger)
        {
            StartCoroutine("BlockYarnMerge");
        }
        if (allowMerger) 
        {
            StartCoroutine("AllowYarnMerger");
        }
    }

    private IEnumerator BlockYarnMerge() //Blocks yarn merge by damaging each existing ball using the Color Controller
    {
        foreach (ColorController yarn in balls)
        {
            yarn.DamageNotDull();
        }
        yield return null;
    }

    private IEnumerator AllowYarnMerge() //Re-Enables yarn merger by repairing each existing ball using the Color Controller
    {
        foreach (ColorController yarn in balls)
        {
            yarn.Repair();
        }
        yield return null;
    }
}
