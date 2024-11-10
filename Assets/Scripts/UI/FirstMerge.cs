using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMerge : MonoBehaviour
{
    private BallCombine ballCombines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable() 
    {
        ballCombines?.OnCombine += Stuff();
    }

    private void OnDisable()
    {
        ballCombines?.OnCombine -= Stuff();
    }
    private void Stuff() 
    {

    }
}
