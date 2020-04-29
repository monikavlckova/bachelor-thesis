using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int level;
    private const float MAX_PROGRESS = 10f;
    private float progress = 0f;

    private Image image;
    /*private Color32[] colorGardient = new[]{
        new Color32(128, 20, 20, 255),
        new Color32(167, 31, 23, 255),
        new Color32(200, 63, 29, 255),
        new Color32(207, 84, 29, 255),
        new Color32(224, 153, 34, 255),
        new Color32(233, 185, 27, 255),
        new Color32(180, 206, 35, 255),
        new Color32(119, 206, 35, 255),
        new Color32(64, 161, 11, 255),
        new Color32(11, 125, 43, 255)
    };*/

    void Start()
    {
        image = GetComponent<Image>();
        if (Data.DATA.createdTask)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        progress = Data.DATA.progressInLvls[level-1];
        //int index = progress < colorGardient.Length ? (int) progress : colorGardient.Length-1;
        //image.color = colorGardient[index];
        if (Data.DATA.changed)
        {
            image.fillAmount = (progress - 1) / MAX_PROGRESS;
            Data.DATA.changed = false;
        }
        else
        {
            image.fillAmount = (progress) / MAX_PROGRESS;
        }
    }

    private void Update()
    {
        progress =  Data.DATA.progressInLvls[level-1];
        //int index = progress < colorGardient.Length ? (int) progress : colorGardient.Length-1;
        //image.color = colorGardient[index];
        if (image.fillAmount <  progress / MAX_PROGRESS)
        {
            image.fillAmount += (0.1f * Time.deltaTime);
        }
        //image.fillAmount = progress / MAX_PROGRESS;
    }
    
    
}
