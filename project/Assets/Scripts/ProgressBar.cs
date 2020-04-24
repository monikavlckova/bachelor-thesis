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
    private Color32[] colorGardient = new[]
    {
        new Color32(185, 18, 27, 255),
        new Color32(178, 52, 19, 255),
        new Color32(172, 92, 19, 255),
        new Color32(165, 128, 20, 255),
        new Color32(158, 159, 21, 255),
        new Color32(117, 153, 21, 255),
        new Color32(79, 146, 21, 255),
        new Color32(45, 140, 22, 255),
        new Color32(22, 134, 29, 255),
        new Color32(22, 128, 57, 255)
    };

    void Start()
    {
        if (Data.DATA.createdTask)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
        progress = Data.DATA.progressInLvls[level-1];
        int index = progress < colorGardient.Length ? (int) progress : colorGardient.Length-1;
        GetComponent<Image>().color = colorGardient[index];
        GetComponent<Image>().fillAmount = progress / MAX_PROGRESS;
    }

    private void Update()
    {
        progress =  Data.DATA.progressInLvls[level-1];
        int index = progress < colorGardient.Length ? (int) progress : colorGardient.Length-1;
        GetComponent<Image>().color = colorGardient[index];
        GetComponent<Image>().fillAmount = progress / MAX_PROGRESS;
    }
    
    
}
