using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySetting : MonoBehaviour
{
    public int defaultMouseSensitivity = 100;
    public Slider mouseSensitivitySlider;
    void Start()
    {
        defaultMouseSensitivity = PlayerPrefs.GetInt("mouseSensitivity", 100);
        mouseSensitivitySlider.value = defaultMouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ValueChangeCheck()
    {
        PlayerPrefs.SetInt("mouseSensitivity", (int)mouseSensitivitySlider.value);
    }
}
