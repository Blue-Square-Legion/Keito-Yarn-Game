using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockMerge : MonoBehaviour
{
    [SerializeField] private List<ColorController> balls = new();
    public CatYarnInteraction interaction;
    public bool stopMerger, allowMerger;

    public GameObject cat;
    // Start is called before the first frame update
    void Start()
    {
        stopMerger = true;
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

    private void OnEnable()
    {
        CheckForCat();
        interaction = cat.GetComponent<CatYarnInteraction>();
        interaction?.OnReject.AddListener(CatYarnRejection);
    }
    private void OnDisable()
    {
        interaction?.OnReject.RemoveListener(CatYarnRejection);
        cat = null;
    }

    private IEnumerator BlockYarnMerge() //Blocks yarn merge by damaging each existing ball using the Color Controller
    {
        foreach (ColorController yarn in balls)
        {
            yarn.DamageNotDull();
            FirstMerge.FM.AddToList(yarn.gameObject.GetComponent<BallCombine>());
        }
        yield return null;
        stopMerger = false;
    }

    private void CheckForCat() 
    {
        if (cat == null)
        {
            Debug.Log("Looking for cat");
            cat = GameObject.FindGameObjectWithTag("Cat");
        }
    }

    private IEnumerator AllowYarnMerger() //Re-Enables yarn merger by repairing each existing ball using the Color Controller
    {
        foreach (ColorController yarn in balls)
        {
            yarn.Repair();
        }
        yield return null;
        this.enabled = false;
    }
    private void CatYarnRejection(RejectType type) 
    {
        if (type.Equals(RejectType.Damage))//The intention is that the yarn balls will not be able to merge until the cat has rejected a ball for being too small
                                           //but because the balls start off damaged, they will be rejected for being damaged first. Effectively the same at the moment
        {
            allowMerger = true;
            RevisedWalkthrough.RW.NextSlide();
        }
    }
}
