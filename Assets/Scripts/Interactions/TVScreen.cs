using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVScreen : Switchable
{
    [SerializeField] private Material offMat;
    [SerializeField] private Material onMat;

    private TVAudio tvAudio;
    private MeshRenderer mRenderer;
    private bool isOn = false;

    void Awake() {
        mRenderer = gameObject.GetComponent<MeshRenderer>();
        tvAudio = gameObject.GetComponent<TVAudio>();
    }

    public override void Switch() {
        if (isOn) {
            mRenderer.material = offMat;
            tvAudio.PlayOn();
        } else {
            mRenderer.material = onMat;
            tvAudio.PlayOff();
        }

        isOn = !isOn;
    }
}
