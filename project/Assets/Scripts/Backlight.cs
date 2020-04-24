using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backlight : MonoBehaviour
{
    public Color OnColor;
    public Color OffColor;
    public bool isOn;

    public void switchBacklight()
    {
        isOn = !isOn;
        this.transform.GetComponent<Image>().color = isOn ? OnColor : OffColor;
    }

    public void turnOffBacklight()
    {
        isOn = false;
        this.transform.GetComponent<Image>().color = OffColor;
    }
}

