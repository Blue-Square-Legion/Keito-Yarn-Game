using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMerge : MonoBehaviour
{
    [SerializeField] private List<BallCombine> ballCombines = new();
    public static FirstMerge FM;
    // Start is called before the first frame update
    void Start()
    {
        if (FM == null)
            FM = this;
    }

    public void AddToList(BallCombine addition) 
    {
        if (!ballCombines.Contains(addition))
        {
            ballCombines.Add(addition);
            addition?.OnCombine.AddListener(CombineDetected);
        }
    }

    private void CombineDetected() 
    {
        Debug.Log("Combine Detected");
        RevisedWalkthrough.RW.NextSlide();
        StartCoroutine(nameof(RemoveAll));
    }

    private IEnumerator RemoveAll() 
    {
        Debug.Log("Starting to remove listners");
        foreach (BallCombine yarn in ballCombines) 
        {
            if (yarn.Equals(null))
            {
                yield return null;
            }
            else
            {
                yarn?.OnCombine.RemoveListener(CombineDetected);
                yield return null;
            }
        }
        yield return null;
        this.enabled = false;
    }
}
