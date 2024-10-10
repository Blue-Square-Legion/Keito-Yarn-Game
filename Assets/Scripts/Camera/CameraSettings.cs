using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings : MonoBehaviour
{
    public Slider cameraSensitivitySlider;
    public TextMeshProUGUI cameraSensitivityText;
    public PlayerPrefSO cameraSO;
    private float cameraSensitivity = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        SetUp();

        cameraSensitivitySlider.onValueChanged.AddListener(UpdateCameraSensitivity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUp()
    {
        if (PlayerPrefs.HasKey(cameraSO.currKey.ToString()))
        {
            cameraSensitivitySlider.value = PlayerPrefs.GetFloat(cameraSO.currKey.ToString());
            cameraSensitivityText.text = PlayerPrefs.GetFloat(cameraSO.currKey.ToString()).ToString();

            AdjustSensitivity(cameraSensitivitySlider.value);
        }
    }

    private void UpdateCameraSensitivity(float value)
    {
        AdjustSensitivity(value);

        cameraSensitivityText.text = value.ToString();

        PlayerPrefs.SetFloat(cameraSO.currKey.ToString(), value);
        PlayerPrefs.Save();
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteKey(cameraSO.currKey.ToString());
        cameraSensitivitySlider.value = cameraSensitivity;
        cameraSensitivityText.text = cameraSensitivity.ToString();
    }

    private void AdjustSensitivity(float value)
    {
        var cmfreelook = FindObjectOfType<CMFreeLookFlip>();
        if (cmfreelook != null)
        {
            cmfreelook.cameraSens = (value / cameraSensitivitySlider.maxValue) * Mathf.Sqrt(cameraSensitivitySlider.maxValue);
        }
        else
        {
            Debug.LogError("no cmfreelook found");
        }
    }
}
