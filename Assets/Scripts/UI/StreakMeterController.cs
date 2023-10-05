using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreakMeterController : MonoBehaviour
{
    public Slider bar0To20;
    public Slider bar20To50;

    public void SetStreakMeterValue(int newValue)
    {
        if (newValue < bar20To50.minValue) bar20To50.gameObject.SetActive(false);
        else bar20To50.gameObject.SetActive(true);
        
        bar0To20.value = newValue;
        bar20To50.value = newValue;
    }
}
