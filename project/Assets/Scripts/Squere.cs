using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squere : MonoBehaviour
{
    private int index = -1;
    public Sprite[] sprites = new Sprite[4];
    private bool longClicked = false;

    public void Click()
    {
        if (!longClicked)
        {

            index += 1;
            if (index > 3) index = -1;

            if (index >= 0)
            {
                transform.parent.transform.GetComponent<Image>().sprite = sprites[index];
            }
            else
            {
                transform.parent.transform.GetComponent<Image>().sprite = null;
            }
        }
        longClicked = false;
    }

    public void LongClick()
    {
        index = -1;
        transform.parent.transform.GetComponent<Image>().sprite = null;
        longClicked = true;
    }
}