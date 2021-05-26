using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator; //heart
    [SerializeField] private Slider _beatsPerMinuteSlider;
    [SerializeField] private TextMeshProUGUI _bloodFlowText;
    [SerializeField] private Canvas _heartCanvas;

    private float _beatsPerMinute = 60; 
    private float _animationSpeedOfOneInBps = 60f / (49f / 30f); //animation takes 49 frames at 30 FPS -> makes 60 / (49/30) bps
    
    private readonly float _bloodPerPumpInLiters = 0.08f;

    private void Start()
    {
        _beatsPerMinuteSlider.value = _beatsPerMinute;
        _beatsPerMinuteSlider.onValueChanged.AddListener(SliderChanged);
        AdjustBloodFlow();
    }

    private void SliderChanged(float value)
    {
        _beatsPerMinute = value;
        AdjustBloodFlow();
    }

    private void AdjustBloodFlow()
    {
        _animator.speed = _beatsPerMinute / _animationSpeedOfOneInBps;
        String activity;
        if(_beatsPerMinute <= 95)
        {
            activity = "little to no";
        } else if(_beatsPerMinute <= 119)
        {
            activity = "a very light";
        } else if(_beatsPerMinute <= 144)
        {
            activity = "a light";
        } else if(_beatsPerMinute <= 164)
        {
            activity = "a moderate";
        } else if(_beatsPerMinute <= 190)
        {
            activity = "a hard";
        } else
        {
            activity = "a maximum";
        }
       
        _bloodFlowText.SetText(
            "Currently, the heart is beating " + 
            Math.Floor(_beatsPerMinute) + 
            " times per minute. It is pumping " + 
            String.Format("{0:0.00}", (_bloodPerPumpInLiters * _beatsPerMinute)).Replace(",", ".") +
            " liters of blood per minute through the entire body. This corresponds to " +
            activity +
            " strain on the heart.");
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        _heartCanvas.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        _beatsPerMinuteSlider.onValueChanged.RemoveAllListeners();
    }
}
