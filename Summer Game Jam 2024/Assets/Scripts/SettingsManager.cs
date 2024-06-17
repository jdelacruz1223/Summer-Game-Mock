using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Slider audioSlider;

    public void SetAudioVolume(float volume) => Manager.GetInstance().SetAudioVolume(audioSlider.value);
}
