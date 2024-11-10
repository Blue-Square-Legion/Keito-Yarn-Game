using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour
{
    /// <summary>
    /// This script is just so that Block Merge does not look for the cat until after the cat has spawned.
    /// OnEnable is called before Start
    /// </summary>
    [SerializeField] private BlockMerge script;
    private float timer = .5f;

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            script.enabled = true;
            this.enabled = false;
        }
        else 
            timer -= Time.deltaTime;
    }
}
