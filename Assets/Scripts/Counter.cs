using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private int currentCount = 0;
    public TextMeshProUGUI counterField;
    public Button decreaseButton;

    public void Start()
    {
        updateCounter();
    }

    public void Increase()
    {
        currentCount++;
        updateCounter();
    }

    public void Decrease()
    {
        if(currentCount > 0)
        {
            currentCount--;
            updateCounter();
        } 
    }
    
    public void ResetCounter()
    {
        currentCount = 0;
        updateCounter();
    }

    private void updateCounter()
    {
        counterField.SetText(currentCount.ToString());
        if(currentCount <= 0)
        {
            decreaseButton.interactable = false;
        } else
        {
            decreaseButton.interactable = true;
        }
    }
}
