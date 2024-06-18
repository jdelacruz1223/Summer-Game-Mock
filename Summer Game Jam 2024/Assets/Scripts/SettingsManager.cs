using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Slider audioSlider;

    public void SetAudioVolume() => Manager.GetInstance().SetAudioVolume(audioSlider.value);
}
