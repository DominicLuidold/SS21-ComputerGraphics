using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toaster : MonoBehaviour
{
    private float _toasterShowingTime = 5;
    [SerializeField] private TextMeshProUGUI _toaster;
    private float _toasterTime;

    void Update()
    {
        if(_toaster.enabled)
        {
            _toasterTime -= Time.deltaTime;
            if(_toasterTime <= 0)
            {
                _toaster.enabled = false;
            }
        }
    }

    public void ShowToast(string text)
    {
        _toaster.SetText(text);
        _toaster.enabled = true;
        _toasterTime = _toasterShowingTime;
    }
}
