using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class VideoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedField;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private VideoPlayer _video;
    [SerializeField] private Toggle _loop;
    private float speed = 1f;
    void Start()
    {
        _speedSlider.value = speed;
        _video.playbackSpeed = speed;
        _speedField.SetText(speed.ToString("0.0") + "x");
        _speedSlider.onValueChanged.AddListener(SliderChanged);
        _loop.onValueChanged.AddListener(ToggleChanged);
    }

    private void ToggleChanged(bool loop)
    {
        _video.isLooping = loop;
    }

    private void SliderChanged( float value)
    {
        _video.playbackSpeed = value;
        _speedField.SetText(value.ToString("0.0") + "x");
    }

    private void OnDestroy()
    {
        _speedSlider.onValueChanged.RemoveAllListeners();
        _loop.onValueChanged.RemoveAllListeners();
    }
}
